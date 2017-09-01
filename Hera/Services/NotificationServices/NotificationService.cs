using Entities.Notifications;
using Hera.Data;
using Hera.Models.NotificationViewModels;
using Hera.Services.NotificationServices.NotificationBuilders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services
{
    public class NotificationService
    {
        private readonly IDataAccess _data;

        public NotificationService(IDataAccess data)
        {
            _data = data;
        }

        public async Task<int> Get_UnreadNotificationsCount(int userId)
        {
            var notifications = (await Get_notifications(userId))
                .ToList();

            return notifications.Count;
        }

        public async Task<IEnumerable<NotificationDateViewModel>>
            Get_Notifications(int userId,
            int skip, int take)
        {
            var notifications =
                (await Get_notifications(userId, false, false, skip, take))
                .GroupBy(n => n.Date.Date)
                .Select(n =>
                new NotificationDateViewModel()
                {
                    Resumed = string.Format("{0:D}", n.Key, new CultureInfo("es-ES")),
                    Notifications = n.ToList()
                });

            return notifications;

        }


        public async Task<IEnumerable<NotificationViewModel>>
            GetResumedNotifications(int userId)
        {
            var data = await
                Get_notifications(userId, true, true);
            var notifications = data
                .GroupBy(n => n.Type)
                .ToDictionary(g => g.Key,
                g => g.ToList());
            return NotificationBuilder.ResumeNotifications(notifications);

        }

        private async Task<IEnumerable<Notification>> Get_notifications(
            int userId, bool unread = true, bool markAsRead = false,
            int skip = 0, int take = 100)
        {
            var notifications = await _data
                .GetAll_Notifications(userId, unread)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            if (markAsRead)
                _data.Do_MarkAsRead(notifications).Wait();

            return notifications;
        }






    }
}
