using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services
{
    public class NotificationSerivce
    {
        string _message;


        public NotificationSerivce()
        {
            _message = Guid.NewGuid().ToString();
        }
     
        public string Message()
        {
            return _message;
        }
    }
}
