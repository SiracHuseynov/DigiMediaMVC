using DigiMedia.Business.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiMedia.Business.Extensions
{
    public static class Helper
    {
        public static string SaveFile(string rootPath, string folder, IFormFile file)
        {
            if (file.ContentType != "image/png")
                throw new ImageContentTypeException("Sekil formati duzgun deyil");

            if (file.Length > 2097152)
                throw new ImageSizeException("Seklin olcusu 2mb ola biler!");
            
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string path = rootPath + $@"\{folder}\" + fileName;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return fileName;

        }

        public static void DeleteFile(string rootPath, string folder, string file)
        {
            string path = rootPath + $@"\{folder}\" + file;

            if (!File.Exists(path))
                throw new FileeNotFoundException("Image Tapilmadi!");

            File.Delete(path);
        }
    }
}
