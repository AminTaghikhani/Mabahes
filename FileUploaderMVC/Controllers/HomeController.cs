using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileUploaderMVC.Models;
using System.Drawing;

namespace FileUploaderMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage() {
            var files = this.Request.Form.Files;
            var urls = new List<string>();
            var outUrls = new List<string>();
            foreach (var file in files) {
                if (file != null) {
                    var path = @"./Uploads/" + file.FileName;
                    var url = "/Uploads/" + file.FileName;
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    urls.Add(url);

                    var bitmap = new Bitmap(path);
                    var pixels = new int[bitmap.Width, bitmap.Height];
                    for (int i = 0;
                         i < bitmap.Width;
                         i++)
                    {
                        for (int j = 0;
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
                    for (int i = 0;
                         i < bitmap.Width;
                         i++)
                    {
                        for (int j = 0;
                             j < bitmap.Height;
                             j++)
                        {
                            outputImage.SetPixel(i, j, Color.FromArgb(pixels[i, j], pixels[i, j], pixels[i, j]));
                        }
                    }
                    var outPath = @"./Uploads/gs/" + file.FileName;
                    var outUrl  = "/Uploads/gs/"   + file.FileName;
                    outputImage.Save(outPath);
                    outUrls.Add(outUrl);
                }
            }

            ViewData["imgUrl"] = urls[0];
            ViewData["imgUrlOut"] = outUrls[0];
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
