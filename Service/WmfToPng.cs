using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WmfToPngServe.Service
{
    public class WmfToPng
    {
        public static Stream Convert(Stream stream)
        {
            using var img = new Metafile(stream);
            Console.WriteLine("width=" + img.Width + ";height=" + img.Height);
            var header = img.GetMetafileHeader();
            var scale = header.DpiX / 96f;
            // 设置缩放倍数
            var multiple = SmartMultiple(img.Width);
            using var bitmap = new Bitmap(img.Width * multiple, img.Height * multiple);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.ScaleTransform(scale * multiple, scale * multiple);
                g.DrawImage(img, 0, 0);
            }

            Stream outStream = new MemoryStream();
            bitmap.Save(outStream, ImageFormat.Png);
            return outStream;
        }

        /**
         * 根据wmf文件本身的尺寸，获取缩放倍数
         */
        private static int SmartMultiple(int width)
        {
            var multiple = 1;
            if (width < 100)
                multiple = 10;
            else if (width < 200) multiple = 5;
            return multiple;
        }
    }
}