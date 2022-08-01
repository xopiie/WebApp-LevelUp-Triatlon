using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppTriathlon.Areas.Identity.Data;
using WebAppTriathlon.Data;

[assembly: HostingStartup(typeof(WebAppTriathlon.Areas.Identity.IdentityHostingStartup))]
namespace WebAppTriathlon.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseMySQL(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;

                })
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<IdentityContext>();
            });
        }
         }
    }
