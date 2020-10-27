using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodingBlog.Models;
using Microsoft.AspNetCore.Http;

namespace CodingBlog.Helpers
{
    public static class ImageHelper
    {
        public static byte[] EncodeImage(IFormFile image)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);
            var output = ms.ToArray();

            ms.Close();
            ms.Dispose();

            return output;
        }

        public static string DecodeImage(byte[] image, string fileName)
        {
            var binary = Convert.ToBase64String(image);
            var ext = Path.GetExtension(fileName);
            return $"data:image/{ext};base64,{binary}";
        }

        public static string GetImage(Post post)
        {
            var binary = Convert.ToBase64String(post.Image);
            var ext = Path.GetExtension(post.FileName);
            return $"data:image/{ext};base64,{binary}";
        }
    }
}
