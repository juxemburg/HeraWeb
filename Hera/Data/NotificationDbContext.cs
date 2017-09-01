using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Hera.Data
{
    public class NotificationDbContext : DbContext
    {

        public DbSet<Notification> Notifications { get; set; }

        public NotificationDbContext(
            DbContextOptions options)
            :base(options)
        {
        }

        
    }
}
