using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

        public static bool CheckImageExtensions(string extention)
        {

            if (ImageExtensions.Contains(extention.ToUpperInvariant()))
            {
                return true;
            }
            return false;

        }
       

        private static string GetFullPath(IFormFile formFile, string guid)
        {


            var pathToSave = Path.Combine(Directory.GetCurrentDirectory());

            string dbPath = GetDBPath(formFile, guid);
            if (dbPath.Contains("Hata"))
            {
                return dbPath;
            }
            return Path.Combine(pathToSave, dbPath);
        }

        private static string GetDBPath(IFormFile formFile, string guid)
        {

            var folderName = Path.Combine("Resources", "CarImages");

            var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
            if (CheckImageExtensions(GetExtensionOfFile(fileName)))
            {
                return Path.Combine(folderName, guid + GetExtensionOfFile(fileName));
            }

            return "Hata Resim Formatı Desteklenmiyor.";
        }
        private static string GetExtensionOfFile(string fileName)
        {
            string[] splitFileName = fileName.Split('.');
            return "." + splitFileName[1];


        }
        private static string SaveToFileDirectory(IFormFile formFile)
        {
            try
            {

                string guid = Guid.NewGuid().ToString();

                string fullPath = GetFullPath(formFile, guid);
                if (fullPath.Contains("Hata"))
                {
                    return fullPath;
                }
                string dbPath = GetDBPath(formFile, guid);
                if (formFile.Length > 0)
                {

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                    return dbPath;
                }
                return "Hata Yüklenecek Dosya bulunamadı.";
            }
            catch (Exception)
            {

                return "Hata Resim Kaydedilirken Hata Oluştu.";
            }


        }
        public static string Save(IFormFile file,string folderPath)
        {
            string dbPath;
            var filePath = GetPathWithNewName(file,folderPath,out dbPath);
            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return dbPath;
        }
        public static IResult Delete(string imageDBPath)
        {
            try
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), imageDBPath));
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }
       
        public static string GetPathWithNewName(IFormFile file,string folderPath, out string dbPath)
        {
            FileInfo ff = new FileInfo(file.FileName);
            string fileExtension = ff.Extension;
           
            string path = Path.Combine(Environment.CurrentDirectory, folderPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            var newName = Guid.NewGuid().ToString()  + fileExtension;
            dbPath = $@"{folderPath}\{newName}"; 
            string fullPath = $@"{path}\{newName}";
            return fullPath;
        }
    }
}
