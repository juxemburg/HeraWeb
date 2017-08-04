using Hera.Data;
using System;
using System.IO;
using System.Linq;

namespace Hera.Services
{
    public class FileManagerService
    {
        private DirectoryInfo _directory;
        private readonly ApplicationDbContext _context;

        public FileManagerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InitializePath()
        {
            _directory = Directory.CreateDirectory("Files/Temp/");
        }

        public string GetFilePath()
        {
            return Guid.NewGuid().ToString();
        }

        public void DeleteFile(string filePath, bool forced = false)
        {
            if (File.Exists(filePath)
                && (Is_referenceFree(filePath) ) )
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

        private bool Is_referenceFree(string filePath)
        {
            return _context.Desafios
                .Where(d => d.DirSolucion.Equals(filePath))
                .Count() <= 1;
        }
    }
}
