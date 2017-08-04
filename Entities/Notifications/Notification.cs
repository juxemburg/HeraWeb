using System;

namespace Entities.Notifications
{
    public class Notification
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public string Key { get; set; }
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public bool Unread { get; set; }
        public NotificationType Type { get; set; }
    }
}
