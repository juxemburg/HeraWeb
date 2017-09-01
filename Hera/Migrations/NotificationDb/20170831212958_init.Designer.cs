using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Hera.Data;
using Entities.Notifications;

namespace Hera.Migrations.NotificationDb
{
    [DbContext(typeof(NotificationDbContext))]
    [Migration("20170831212958_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Notifications.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Key");

                    b.Property<string>("Message");

                    b.Property<int>("Type");

                    b.Property<bool>("Unread");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });
        }
    }
}
