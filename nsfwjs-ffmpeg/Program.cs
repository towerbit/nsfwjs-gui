using FFmpeg.AutoGen;
using FFmpeg.AutoGen.Example;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

            var url = "http://frp.evenstandard.top:23877/nsfw";
            var src = ""; // "rtsp://admin:pass@19.0.1.117/H264?ch=1&subtype=0";
            var output = "d:\\temp\\nsfwjs_output";
            var iSkip = 10000;
            var fProb = 0.9F;
            var iGPU = 0;
            foreach (string arg in args)
            {
                if (arg.StartsWith("--src=", StringComparison.OrdinalIgnoreCase))
                    src = arg.Substring("--src=".Length);
                if (arg.StartsWith("--host=", StringComparison.OrdinalIgnoreCase))
                    url = $"http://{arg.Substring("--host=".Length)}:23877/nsfw";
                if (arg.StartsWith("--output=", StringComparison.OrdinalIgnoreCase))
                    output = arg.Substring("--output=".Length);
                if (arg.StartsWith("--skip=", StringComparison.OrdinalIgnoreCase))
                    iSkip = int.Parse(arg.Substring("--skip=".Length));
                if (arg.StartsWith("--prob=", StringComparison.OrdinalIgnoreCase))
                    fProb = float.Parse(arg.Substring("--prob=".Length));
                if (arg.StartsWith("--gpu=", StringComparison.OrdinalIgnoreCase))
                    iGPU = int.Parse(arg.Substring("--gpu=".Length));
            }

            SetupLogging();
            ConfigureHWDecoder(iGPU, out var deviceType);

            if (string.IsNullOrEmpty(src))
            {
                Console.WriteLine($"Failed for --src is empty");
                Environment.Exit(-10000);
            }
            
            output = Path.Combine(output, validFilename(src));
            if(!Directory.Exists(output))
                try
                {
                    Directory.CreateDirectory(output);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create output folder {output}, err: {ex.Message}");
                    Environment.Exit(-10001); 
                }
            
            Console.WriteLine($"====\r\nsrc:\t{src}\r\nurl:\t{url}\r\nout:\t{output}\r\nskip:\t{iSkip}\r\nprob:\t{fProb}\r\n===="); 

            var client = new RestSharp.RestClient(url);
            var cts = new CancellationTokenSource();
            var iCount = 0;

            if (File.Exists(src) || 
                src.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || 
                src.StartsWith("rtsp://", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Decoding {src}...");
                DecodeAllFramesToImages(deviceType, src, cts.Token, 
                    (bmp, num, sec) =>
                    {
                        if (postBitmap(bmp, num, client, output, fProb, sec, ref iCount))
                        { 
                            if (iCount >= iSkip)
                            {
                                cts?.Cancel();
                            }
                        }
                    });
            }
            else
            {
                Console.WriteLine($"{src} not found or supported");
                iCount = -10002;
            }

            Environment.Exit(iCount);
        }
        /// <summary>
        /// deletegate for got bitmap
        /// </summary>
        /// <param name="bmp">bitmap converted from current frame</param>
        /// <param name="frameId">current frame id</param>
        /// <param name="timestamp">played seconds</param>
        delegate void dGotBitmap(Bitmap bmp, int frameId, int timestamp);
        unsafe delegate void dGotYuvData(AVCodecContext pCodexCtx, AVFrame pFrame);

        private static bool postBitmap(Bitmap bmp, int num, RestSharp.IRestClient client, string output, float fProb, int sec, ref int iCount)
        {
            /* convert to JPEG stream */
            //using (var ms = new MemoryStream())
            //{
            //    bmp.Save(ms, ImageFormat.Jpeg);

            /* or Convert to JPEG Thumbnail stream, reduce file size for less post */
            using (var ms= JpegCovertHelper.GetStream(bmp, new Size(640,360), JpegCovertHelper.SizeMode.AutoZoom))
            { 
                var buff = ms.ToArray();
                var filename = $"{num:D8}.jpg";
                var request = new RestSharp.RestRequest(RestSharp.Method.POST);
                request.AddFileBytes("image", buff, filename, "multipart/form-data");

                var response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = JsonSerializer.Deserialize<List<dto_nsfwjs_result>>(response.Content);
                    dto_nsfwjs_result max = new dto_nsfwjs_result("Nothing", 0);
                    foreach (dto_nsfwjs_result item in result)
                        if (item.probability >= max.probability)
                            max = item;

                    if (max.probability > fProb && (max.className == "Porn" || max.className == "Hentai"))
                    {
                        /* save matched frame */
                        filename = Path.Combine(output, filename);
                        //File.WriteAllBytes(filename, buff); //save compress stream to file
                        
                        /* mark timestamp on bmp */
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            TimeSpan ts = new TimeSpan(0, 0, sec);
                            g.DrawString($"{ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}", new Font("YaHei", 12F, FontStyle.Bold), Brushes.Green, new PointF(10, 10));
                        }
                        /* save source bmp to JPEG */
                        bmp.Save(filename, ImageFormat.Jpeg);

                        iCount++;
                        Console.WriteLine($"= WARN = Matched {iCount} for {max.className}, saved to {filename}");
                    }
                    Console.WriteLine($"frameId:{num}, {max}");

                    return true;
                }
                else
                    Console.WriteLine($"!!! RestSharp.Client response.StatusCode = {response.StatusCode}");
            }
            return false;
        }
        private record dto_nsfwjs_result(string className, double probability);

        private static string validFilename(string src)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                src = src.Replace(c, '_');
            //foreach (char c in Path.GetInvalidPathChars())
            //    src=src.Replace(c, '_');
            return src;
        }

        private static void ConfigureHWDecoder(int inputDecoderNumber, out AVHWDeviceType HWtype)
        {
            HWtype = AVHWDeviceType.AV_HWDEVICE_TYPE_NONE;
            //return;
            //Console.WriteLine("Use hardware acceleration for decoding?[n]");
            //var key = Console.ReadLine();
            var availableHWDecoders = new Dictionary<int, AVHWDeviceType>();
            //if (key == "y")
            {
                //Console.WriteLine("Select hardware decoder:");
                var type = AVHWDeviceType.AV_HWDEVICE_TYPE_NONE;
                var number = 0;

                //var sb = new StringBuilder();
                while ((type = ffmpeg.av_hwdevice_iterate_types(type)) != AVHWDeviceType.AV_HWDEVICE_TYPE_NONE)
                {
                    Console.WriteLine($"{++number}. {type}");
                    availableHWDecoders.Add(number, type);
                    //sb.AppendLine($"{number}.{type}");
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
                //Console.WriteLine($"Selected [{decoderNumber}]");
                //int.TryParse(Console.ReadLine(), out var inputDecoderNumber);

                //var input = Microsoft.VisualBasic.Interaction.InputBox(sb.ToString(),
                //    "Use hardware acceleration for decoding?", "0");
                //int.TryParse(input, out var inputDecoderNumber);
                //availableHWDecoders.TryGetValue(inputDecoderNumber == 0 ? decoderNumber : inputDecoderNumber, out HWtype);
                availableHWDecoders.TryGetValue(inputDecoderNumber, out HWtype);
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
                Console.WriteLine($"hw device : {HWDevice}");
                var info = vsd.GetContextInfo();
                info.ToList().ForEach(x => Console.WriteLine($"{x.Key} = {x.Value}"));

                var sourceSize = vsd.FrameSize;
                var sourcePixelFormat = HWDevice == AVHWDeviceType.AV_HWDEVICE_TYPE_NONE ? vsd.PixelFormat : GetHWPixelFormat(HWDevice);
                var destinationSize = sourceSize;
                var destinationPixelFormat = AVPixelFormat.AV_PIX_FMT_BGR24;
                using (var vfc = new VideoFrameConverter(sourceSize, sourcePixelFormat, destinationSize, destinationPixelFormat))
                {
                    var frameId = 0;
                    
                    while (!cancellationToken.IsCancellationRequested && vsd.TryDecodeNextFrame(out var frame))
                    {
                        AVFrame convertedFrame = vfc.Convert(frame);

                        if (null != fGotBitmap)
                        {
                            if (frame.key_frame == 1)
                                using (var bitmap = new Bitmap(convertedFrame.width, convertedFrame.height, convertedFrame.linesize[0], PixelFormat.Format24bppRgb, (IntPtr)convertedFrame.data[0]))
                                {
                                    var den = frame.pkt_duration * vsd.FrameRate;
                                    int sec=(int)(frame.best_effort_timestamp / den);
                                    fGotBitmap(bitmap, frameId, sec);
                                }
                        }
                        frameId++;
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
