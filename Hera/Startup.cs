using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hera.Data;
using Entities.Usuarios;
using Hera.Services;
using Microsoft.AspNetCore.Mvc;
using Hera.Services.DesafiosServices;
using Hera.Services.MessageServices;
using Hera.Services.UserServices;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Hera
{
    public class Startup
    {
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (_env.IsDevelopment())
            {

                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration
                .GetConnectionString("HERAdb")));
            services.AddSingleton<FileManagerService>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(config =>
            {
                config.Password.RequireNonAlphanumeric = false;
            });
            services.AddScoped<NotificationSerivce>();
            services.AddScoped<IDataAccess, DataAccess_Sql>();
            services.AddScoped<DesafioService>();
            services.AddTransient<DbSeeder>();
            services.AddMvc(opt =>
            {
                if (!_env.IsProduction())
                {
                    opt.SslPort = 44352;
                }
                opt.Filters.Add(new RequireHttpsAttribute());

            });
            services.AddSingleton<ITempDataProvider,
                CookieTempDataProvider>();
            // Add application services.
            services.AddScoped<UserService>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<SendgridSenderOptions>(Configuration.GetSection("Sendgrid"));
            services.AddScoped<ScratchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            DbSeeder seeder,
            FileManagerService fileManager)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            seeder.Seed().Wait();
            //seeder.ClearDatabase().Wait();
            fileManager.InitializePath();
        }
    }
}
