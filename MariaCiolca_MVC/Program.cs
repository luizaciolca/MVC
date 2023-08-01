using MariaCiolca_MVC.Controllers;
using MariaCiolca_MVC.Data;
using MariaCiolca_MVC.Models;
using MariaCiolca_MVC.Models.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace MariaCiolca_MVC
{
    public class Program
    {

        public void ConfigureLogging(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File("app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });
                } 


        public static void Main(string[] args)
        {
             var builder = WebApplication.CreateBuilder(args);
           //builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //  Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            var ClubLibraConnectionString = builder.Configuration.GetConnectionString("ClubLibraConnection");
            builder.Services.AddDbContext<ClubLibraContext>(options =>
            options.UseSqlServer(ClubLibraConnectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        

            //builder.Services.AddSingleton<LoginLogger>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

 

            //inregistrare repositories

            builder.Services.AddTransient<AnnouncementRepository,AnnouncementRepository>();

            builder.Services.AddTransient<CodeSnippetRepository,CodeSnippetRepository>(); //se creaza ob noi

            builder.Services.AddTransient<MembersRepository, MembersRepository>();

            builder.Services.AddTransient<MembershipTypeRepository, MembershipTypeRepository>();

            builder.Services.AddTransient<MembershipsRepository, MembershipsRepository>();

            builder.Services.AddRazorPages().AddNToastNotifyNoty(new NToastNotify.NotyOptions
            {
                ProgressBar = true,
                Timeout = 300
            }
            );


            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            

            var app = builder.Build();


 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

             app.MapControllerRoute(
     name: "create-codesnippet",
     pattern: "CodeSnippet/Create",
     defaults: new { controller = "CodeSnippet", action = "Create" });

  
            app.UseNToastNotify();

            app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


            app.MapRazorPages();

            app.Run();

    }

    }
}