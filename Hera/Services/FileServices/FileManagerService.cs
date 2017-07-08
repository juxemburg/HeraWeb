using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services
{
    public class FileManagerService
    {
        private DirectoryInfo _directory;

        public FileManagerService()
        {
            
        }

        public void InitializePath()
        {
            _directory = Directory.CreateDirectory("Files/Temp/");
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

        public void DeleteAllFiles()
        {
            string[] filePaths = _directory.GetFiles()
                .Select(f => f.FullName)
                .ToArray();
            foreach (var item in filePaths)
            {
                DeleteFile(item);
            }

        }
    }
}
