using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace nsfwjs_ffmpeg
{
    /// <summary>
    /// Jpeg 压缩辅助类
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

        //JPEG 压缩参数
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
                case SizeMode.Both://指定高宽缩放（可能变形）                 
                    break;
                case SizeMode.ByWidth://指定宽，高按比例                     
                    toheight = original.Height * width / original.Width;
                    break;
                case SizeMode.ByHeight://指定高，宽按比例 
                    towidth = original.Width * height / original.Height;
                    break;
                case SizeMode.AutoZoom://指定高宽裁减（不变形）                 
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

            //新建一个bmp图片 
            thumbnail = new Bitmap(towidth, toheight);

            //新建一个画板 
            using (var g = Graphics.FromImage(thumbnail))
            {
                //设置高质量插值法 
                g.InterpolationMode = InterpolationMode.Low;

                //设置高质量,低速度呈现平滑程度 
                g.SmoothingMode = SmoothingMode.HighSpeed;

                //清空画布并以透明背景色填充 
                g.Clear(Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分 
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
