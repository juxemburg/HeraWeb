using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services
{
    public class FileManagerService
    {
        public string GetFilePath()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
