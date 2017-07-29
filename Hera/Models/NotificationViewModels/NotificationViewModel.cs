using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.NotificationViewModels
{
    public class NotificationViewModel
    {
        public string Message { get; set; }
        public string Action { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }

    }
}
