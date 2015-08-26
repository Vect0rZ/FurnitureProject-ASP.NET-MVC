using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;

namespace FurnitureProject.Common.Managers
{
    public class FileManager : IFileManager
    {
        private const string DEFAULT_PATH = "~/Content/Images/";
        private string[] AllowedExtensions = {
                                                ".png", ".jpg", ".jpeg", ".gif"
                                             };

        public string Save(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName);

            if(ValidateExtension(fileExtension) == false)
            {
                return string.Empty;
            }

            string fileNameEncode = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.Ticks.ToString();
            string fileName = fileNameEncode + fileExtension;
            string path = Path.Combine(HttpContext.Current.Server.MapPath(DEFAULT_PATH), fileName);

            file.SaveAs(path);

            return path;
        }

        public bool DeleteFile(string filePath)
        {
            bool result = false;

            if(String.IsNullOrWhiteSpace(filePath) == false)
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }
            
            return result;
        }

        private bool ValidateExtension(string extension)
        {
            for(int i = 0; i < AllowedExtensions.Length; i++)
            {
                if(extension.Equals(AllowedExtensions[i]) == true)
                {
                    return true;
                }
            }

            return false;
        }

        
    }
}