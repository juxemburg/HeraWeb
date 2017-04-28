using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services
{
    public class FileManagerService
    {
        public void InitializePath()
        {
            DirectoryInfo di = Directory.CreateDirectory("Files/Temp/");
        }

        public string GetFilePath()
        {
            return Guid.NewGuid().ToString();
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
