using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services.ApplicationServices
{
    public class ApplicationServicesException : Exception
    {
        public ApplicationServicesException()
        {
        }

        public ApplicationServicesException(string message) 
            : base(message)
        {
        }

        public ApplicationServicesException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
