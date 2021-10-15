using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace nsfwjs_ffmpeg
{
    /// <summary>
    /// Jpeg Compress Helper
    /// </summary>
    public sealed class JpegCovertHelper
    {
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }

        //JPEG Compress Parameters
        private static EncoderParameters eps = new EncoderParameters(1);
        private static EncoderParameter ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 35L);
        private static ImageCodecInfo jpsEncodeer = GetEncoder(ImageFormat.Jpeg);

        static JpegCovertHelper()
        {
            eps.Param[0] = ep;
        }

        public static System.IO.MemoryStream GetStream(Bitmap bitmap)
        {
            var ms = new System.IO.MemoryStream();
            bitmap.Save(ms, jpsEncodeer, eps);
            return ms;
        }

        public static System.IO.MemoryStream GetStream(Bitmap bitmap, Size newSize, SizeMode mode = SizeMode.Both)
        {
            createThumbnail(bitmap, out Bitmap thumb, newSize.Width, newSize.Height, mode);
            var ms = new System.IO.MemoryStream();
            thumb.Save(ms, jpsEncodeer, eps);
            return ms;
        }

        public enum SizeMode
        {
            Both,
            ByWidth,
            ByHeight,
            AutoZoom
        }

        private static void createThumbnail(Bitmap original, out Bitmap thumbnail, int width, int height, SizeMode mode)
        {
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = original.Width;
            int oh = original.Height;

            switch (mode)
            {
                case SizeMode.Both://fixed width & heigh, ratio maybe changed                 
                    break;
                case SizeMode.ByWidth://fixed width，keep ratio
                    toheight = original.Height * width / original.Width;
                    break;
                case SizeMode.ByHeight://fixed height，keep ration
                    towidth = original.Width * height / original.Height;
                    break;
                case SizeMode.AutoZoom://autofit width & height, keep ratio                 
                    if ((double)original.Width / (double)original.Height > (double)towidth / (double)toheight)
                    {
                        oh = original.Height;
                        ow = original.Height * towidth / toheight;
                        y = 0;
                        x = (original.Width - ow) / 2;
                    }
                    else
                    {
                        ow = original.Width;
                        oh = original.Width * height / towidth;
                        x = 0;
                        y = (original.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            thumbnail = new Bitmap(towidth, toheight);
            using (var g = Graphics.FromImage(thumbnail))
            {
                g.InterpolationMode = InterpolationMode.Low;
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.Clear(Color.Transparent);

                g.DrawImage(original,
                            new Rectangle(0, 0, towidth, toheight),
                            new Rectangle(x, y, ow, oh),
                            GraphicsUnit.Pixel);
                //g.DrawString(DateTime.Now.ToString(),
                //             new Font("YaHei", 8.0F, FontStyle.Bold),
                //             new SolidBrush(Color.YellowGreen),
                //             new PointF(0, 0)); 
            }
        }
    }
}
