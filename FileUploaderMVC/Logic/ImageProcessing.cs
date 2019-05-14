using System;
using System.Drawing;
using System.IO;

namespace FileUploaderMVC.Logic {

    public class ImageProcessing {

        public static int Thresholding(int pixel,
                                       int threshold) => pixel > threshold
                                                             ? 255
                                                             : 0;

        public static string Threshold(string path, int thresholdValue=100) {
            var bitmap = new Bitmap(path);
            var pixels = new int[bitmap.Width, bitmap.Height];
            for (var i = 0;
                 i < bitmap.Width;
                 i++)
            {
                for (var j = 0;
                     j < bitmap.Height;
                     j++)
                {
                    var pixel = bitmap.GetPixel(i,
                                                j);
                    var gs = (pixel.R + pixel.G + pixel.B) / 3;
                    pixels[i, j] = Thresholding(gs,
                                                thresholdValue);
                }
            }
            var outputImage = new Bitmap(bitmap.Width, bitmap.Height);
            for (var i = 0;
                 i < bitmap.Width;
                 i++)
            {
                for (var j = 0;
                     j < bitmap.Height;
                     j++)
                {
                    outputImage.SetPixel(i, j, Color.FromArgb(pixels[i, j], pixels[i, j], pixels[i, j]));
                }
            }

            var fileName = Guid.NewGuid()   + Path.GetExtension(path);
            var outPath  = @"./Uploads/tr/" + fileName;
            var outUrl   = "/Uploads/tr/"   + fileName;
            outputImage.Save(outPath);
            return outUrl;
        }

        public static string GrayScale(string path) {
            var fileName = Guid.NewGuid() + Path.GetExtension(path);
            var bitmap = new Bitmap(path);
            var pixels = new int[bitmap.Width, bitmap.Height];
            for (var i = 0;
                 i < bitmap.Width;
                 i++)
            {
                for (var j = 0;
                     j < bitmap.Height;
                     j++)
                {
                    var pixel = bitmap.GetPixel(i,
                                                j);
                    var gs = (pixel.R + pixel.G + pixel.B) / 3;
                    pixels[i,
                           j] = gs;
                }
            }

            var outputImage = new Bitmap(bitmap.Width, bitmap.Height);
            for (var i = 0;
                 i < bitmap.Width;
                 i++)
            {
                for (var j = 0;
                     j < bitmap.Height;
                     j++)
                {
                    outputImage.SetPixel(i, j, Color.FromArgb(pixels[i, j], pixels[i, j], pixels[i, j]));
                }
            }
            var outPath = @"./Uploads/gs/" + fileName;
            var outUrl  = "/Uploads/gs/"   + fileName;
            outputImage.Save(outPath);

            return outUrl;
        }

    }

}