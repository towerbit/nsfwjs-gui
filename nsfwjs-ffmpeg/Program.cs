using FFmpeg.AutoGen;
using FFmpeg.AutoGen.Example;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;

namespace nsfwjs_ffmpeg
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Current directory: " + Environment.CurrentDirectory);
            Console.WriteLine("Running in {0}-bit mode.", Environment.Is64BitProcess ? "64" : "32");

            FFmpegBinariesHelper.RegisterFFmpegBinaries();

            Console.WriteLine($"FFmpeg version info: {ffmpeg.av_version_info()}");

            SetupLogging();
            ConfigureHWDecoder(out var deviceType);

            var url = "http://frp.evenstandard.top:23333/single/multipart-form";
            var src = "rtsp://admin:pass@19.0.1.117/H264?ch=1&subtype=0";
            var output = "d:\\temp\\nsfwjs_output";

            foreach (string arg in args)
            {
                if (arg.StartsWith("--src=", StringComparison.OrdinalIgnoreCase))
                    src = arg.Substring("--src=".Length);
                if (arg.StartsWith("--local", StringComparison.OrdinalIgnoreCase))
                    url = "http://localhost:23877/nsfw";
                if (arg.StartsWith("--output=", StringComparison.OrdinalIgnoreCase))
                    output = arg.Substring("--output=".Length);
            }
            output = Path.Combine(output, validFilename(src));

            var client = new RestSharp.RestClient(url);
            var cts = new CancellationTokenSource();


            if (!string.IsNullOrEmpty(src) && (File.Exists(src) || src.StartsWith("http://") || src.StartsWith("rtsp://")))
            {
                Console.WriteLine($"Decoding {src}...");
                var iCount = 0;
                if (url.Equals("http://localhost:23877/nsfw"))
                {
                    DecodeAllFramesToImages(deviceType, src, cts.Token, (bmp, num) =>
                    {
                        if (!postBitmapLocal(bmp, num, client, output, ref iCount))
                        {
                            cts?.Cancel();
                            Console.WriteLine("postBitmapLocal return false");
                        }
                        else
                            Console.WriteLine($"postBitmapLocal matched {iCount} times.");
                    });
                }
                else
                {
                    DecodeAllFramesToImages(deviceType, src, cts.Token, (bmp, num) =>
                    {
                        if (!postBitmapRemote(bmp, num, client, output, ref iCount))
                        {
                            cts?.Cancel();
                            Console.WriteLine("postBitmapRemote return false");
                        }
                        else
                            Console.WriteLine($"postBitmapRemote matched {iCount} times.");
                    });
                }

            }
            else
                Console.WriteLine($"{src} File not found");

#if START_NSFWJS
            cts?.Cancel();
            p?.Kill();
#endif
            //Console.WriteLine("Encoding...");
            //EncodeImagesToH264();
        }

        delegate void dGotBitmap(Bitmap bmp, int frameNum);
        //unsafe delegate void dGotYuvData(int width, int height, IntPtr out_buffer_video, int out_buffer_video_size, int pitch);
        unsafe delegate void dGotYuvData(AVCodecContext pCodexCtx, AVFrame pFrame);

        private static bool postBitmapLocal(Bitmap bmp, int num, RestSharp.IRestClient client, string output, ref int iCount)
        {
            using (var ms = new MemoryStream())
            {
                //直接转JPEG
                bmp.Save(ms, ImageFormat.Jpeg);
                //或者生成JPEG缩略图, 减小文件尺寸和大小

                var buff = ms.ToArray();
                var filename = $"frame{num:D8}.jpg";
                var request = new RestSharp.RestRequest(RestSharp.Method.POST);
                request.AddFileBytes("image", buff, filename, "multipart/form-data");

                var response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonSerializer.Deserialize<List<dto_nsfwjs_result>>(response.Content);
                    dto_nsfwjs_result max = new dto_nsfwjs_result("Nothing", 0);
                    foreach (dto_nsfwjs_result item in result)
                        if (item.probability > max.probability)
                            max = item;
                    Console.WriteLine($"{max}");

                    if (max.probability > 0.9 && (max.className == "Porn" || max.className == "Hentai"))
                    {
                        //save matched frame
                        filename = Path.Combine(output, filename);
                        File.WriteAllBytes(filename, buff);
                        iCount++;
                        Console.WriteLine($"===== WARN ===== A {max.className} snapshot saved to {filename}");
                    }

                    return true;
                }
            }
            return false;
        }
        private record dto_nsfwjs_result(string className, double probability);

        /// <summary>
        /// post to "http://frp.evenstandard.top:23333/single/multipart-form"
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="num"></param>
        /// <param name="client"></param>
        /// <param name="output"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        private static bool postBitmapRemote(Bitmap bmp, int num, RestSharp.IRestClient client, string output, ref int iCount)
        {
            using (var ms = new MemoryStream())
            {
                //convert to jpeg stream
                bmp.Save(ms, ImageFormat.Jpeg);
                //or create thumbnail for less size
                //...

                var buff = ms.ToArray();
                var filename = $"frame{num:D8}.jpg";
                var request = new RestSharp.RestRequest(RestSharp.Method.POST);
                request.AddFileBytes("content", buff, filename, "multipart/form-data");

                var response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonSerializer.Deserialize<dto_prediction>(response.Content);
                    dto_nsfwjs_result max = new dto_nsfwjs_result("Nothing", 0);
                    foreach (dto_nsfwjs_result item in result.prediction)
                        if (item.probability > max.probability)
                            max = item;
                    Console.WriteLine($"{max}");

                    if (max.probability > 0.9 && (max.className == "Porn" || max.className == "Hentai"))
                    {
                        filename = Path.Combine(output, filename);
                        File.WriteAllBytes(filename, buff); ;
                        iCount++;
                        Console.WriteLine($"===== WARN ===== A {max.className} snapshot saved to {filename}");
                    }
                    return true;
                }
            }
            return false;
        }
        private record dto_prediction(dto_nsfwjs_result[] prediction);

        private static string validFilename(string src)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                src = src.Replace(c, '_');
            //foreach (char c in Path.GetInvalidPathChars())
            //    src=src.Replace(c, '_');
            return src;
        }
        private static void ConfigureHWDecoder(out AVHWDeviceType HWtype)
        {
            HWtype = AVHWDeviceType.AV_HWDEVICE_TYPE_NONE;
            return;
            Console.WriteLine("Use hardware acceleration for decoding?[n]");
            var key = Console.ReadLine();
            var availableHWDecoders = new Dictionary<int, AVHWDeviceType>();
            if (key == "y")
            {
                Console.WriteLine("Select hardware decoder:");
                var type = AVHWDeviceType.AV_HWDEVICE_TYPE_NONE;
                var number = 0;
                while ((type = ffmpeg.av_hwdevice_iterate_types(type)) != AVHWDeviceType.AV_HWDEVICE_TYPE_NONE)
                {
                    Console.WriteLine($"{++number}. {type}");
                    availableHWDecoders.Add(number, type);
                }
                if (availableHWDecoders.Count == 0)
                {
                    Console.WriteLine("Your system have no hardware decoders.");
                    HWtype = AVHWDeviceType.AV_HWDEVICE_TYPE_NONE;
                    return;
                }
                int decoderNumber = availableHWDecoders.SingleOrDefault(t => t.Value == AVHWDeviceType.AV_HWDEVICE_TYPE_DXVA2).Key;
                if (decoderNumber == 0)
                    decoderNumber = availableHWDecoders.First().Key;
                Console.WriteLine($"Selected [{decoderNumber}]");
                int.TryParse(Console.ReadLine(), out var inputDecoderNumber);
                availableHWDecoders.TryGetValue(inputDecoderNumber == 0 ? decoderNumber : inputDecoderNumber, out HWtype);
            }
        }

        private static unsafe void SetupLogging()
        {
            ffmpeg.av_log_set_level(ffmpeg.AV_LOG_VERBOSE);

            // do not convert to local function
            av_log_set_callback_callback logCallback = (p0, level, format, vl) =>
            {
                if (level > ffmpeg.av_log_get_level()) return;

                var lineSize = 1024;
                var lineBuffer = stackalloc byte[lineSize];
                var printPrefix = 1;
                ffmpeg.av_log_format_line(p0, level, format, vl, lineBuffer, lineSize, &printPrefix);
                var line = Marshal.PtrToStringAnsi((IntPtr)lineBuffer);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(line);
                Console.ResetColor();
            };

            ffmpeg.av_log_set_callback(logCallback);
        }

        /// <summary>
        /// decode all frames to images
        /// </summary>
        /// <param name="HWDevice"></param>
        /// <param name="url"> decode all frames from url, please not it might local resorce, e.g. string url = "../../sample_mpeg4.mp4";</param>
        /// <param name="fGotBitmap"></param>
        private static unsafe void DecodeAllFramesToImages(AVHWDeviceType HWDevice, string url, CancellationToken cancellationToken, dGotBitmap fGotBitmap)
        {
            using (var vsd = new VideoStreamDecoder(url, HWDevice))
            {
                Console.WriteLine($"codec name: {vsd.CodecName}");

                var info = vsd.GetContextInfo();
                info.ToList().ForEach(x => Console.WriteLine($"{x.Key} = {x.Value}"));

                var sourceSize = vsd.FrameSize;
                var sourcePixelFormat = HWDevice == AVHWDeviceType.AV_HWDEVICE_TYPE_NONE ? vsd.PixelFormat : GetHWPixelFormat(HWDevice);
                var destinationSize = sourceSize;
                var destinationPixelFormat = AVPixelFormat.AV_PIX_FMT_BGR24;
                using (var vfc = new VideoFrameConverter(sourceSize, sourcePixelFormat, destinationSize, destinationPixelFormat))
                {
                    var frameNumber = 0;
                    while (!cancellationToken.IsCancellationRequested && vsd.TryDecodeNextFrame(out var frame))
                    {
                        var convertedFrame = vfc.Convert(frame);

                        if (null != fGotBitmap)
                        {
                            if (frame.key_frame == 1)
                                using (var bitmap = new Bitmap(convertedFrame.width, convertedFrame.height, convertedFrame.linesize[0], PixelFormat.Format24bppRgb, (IntPtr)convertedFrame.data[0]))
                                {
                                    //bitmap.Save($"d:\\temp\\frame.{frameNumber:D8}.jpg", ImageFormat.Jpeg);
                                    fGotBitmap(bitmap, frameNumber);
                                }
                        }
                        //Console.WriteLine($"frame: {frameNumber}");
                        frameNumber++;
                    }
                }
            }
        }

        private static AVPixelFormat GetHWPixelFormat(AVHWDeviceType hWDevice)
        {
            switch (hWDevice)
            {
                case AVHWDeviceType.AV_HWDEVICE_TYPE_NONE:
                    return AVPixelFormat.AV_PIX_FMT_NONE;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_VDPAU:
                    return AVPixelFormat.AV_PIX_FMT_VDPAU;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_CUDA:
                    return AVPixelFormat.AV_PIX_FMT_CUDA;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_VAAPI:
                    return AVPixelFormat.AV_PIX_FMT_VAAPI;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_DXVA2:
                    return AVPixelFormat.AV_PIX_FMT_NV12;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_QSV:
                    return AVPixelFormat.AV_PIX_FMT_QSV;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_VIDEOTOOLBOX:
                    return AVPixelFormat.AV_PIX_FMT_VIDEOTOOLBOX;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_D3D11VA:
                    return AVPixelFormat.AV_PIX_FMT_NV12;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_DRM:
                    return AVPixelFormat.AV_PIX_FMT_DRM_PRIME;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_OPENCL:
                    return AVPixelFormat.AV_PIX_FMT_OPENCL;
                case AVHWDeviceType.AV_HWDEVICE_TYPE_MEDIACODEC:
                    return AVPixelFormat.AV_PIX_FMT_MEDIACODEC;
                default:
                    return AVPixelFormat.AV_PIX_FMT_NONE;
            }
        }
    }
}
