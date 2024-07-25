using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pmm.core.Configuration;
using pmm.data.Configuration;
using System;
using NLog;

namespace pmm.server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHibernateWebContext(builder.Configuration.GetConnectionString("pmm"))
                .AddMappings()
                .AddServices();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            try
            {
                app.Services.PerformDatabaseMigrations();
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Fatal(ex, "Database migration failed");
                Environment.FailFast("Database migration failed");
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllers();
            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
