using Entities.Notifications;
using Hera.Data;
using Hera.Services.UserServices;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
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
            var notifications = await Get_unreadNotifications(userId).
                ToListAsync();
            return notifications.Count;
        }

        private IQueryable<Notification> Get_unreadNotifications(int userId)
        {
            return _data.GetAll_Notifications(userId);
        }



        

        
    }
}
