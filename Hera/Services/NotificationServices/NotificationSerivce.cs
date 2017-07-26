using Entities.Notifications;
using Hera.Data;
using Hera.Models.NotificationViewModels;
using Hera.Services.NotificationServices.NotificationBuilders;
using Hera.Services.UserServices;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services
{
    public class NotificationSerivce
    {
        private IDataAccess _data;

        public NotificationSerivce(IDataAccess data)
        {
            _data = data;
        }

        public async Task<int> Get_UnreadNotificationsCount(int userId)
        {
            var notifications = await Get_notifications(userId).
                ToListAsync();
            return notifications.Count;
        }

        

        public async Task<IEnumerable<NotificationViewModel>>
            GetResumedNotifications(int userId)
        {
            var notifications = await
                Get_notifications(userId)
                .GroupBy(n => n.Type)
                .ToDictionaryAsync(g => g.Key,
                g => g.ToList());
            return NotificationBuilder.ResumeNotifications(notifications);

        }

        private IQueryable<Notification> Get_notifications(int userId,
            bool unread = true)
        {
            return _data.GetAll_Notifications(userId, true);
        }



        

        
    }
}
