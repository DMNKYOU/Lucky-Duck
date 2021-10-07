using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using LuckyDucky.Lottery.Interfaces;
using LuckyDucky.MailService;
using LuckyDucky.MailService.Interfaces;
using LuckyDucky.MVC.Areas.Identity.Data;
using LuckyDucky.MVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LuckyDucky.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
//
            //services.ConfigureApplicationCookie(options =>
            ////{
            //    options.Cookie.Name = ".AlwaysTakePartFrom";
            //    options.ExpireTimeSpan = TimeSpan.FromDays(30);
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.SlidingExpiration = true;
            //    //options.Cookie.SecurePolicy
            //});

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".LuckyDucky.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(15);

            });


            

            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddTransient<IMailService, MailService.EmailService>();
            services.AddTransient<ILotteryService, Lottery.LotteryService>();

            services.AddMvc()
                          .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opt => opt.ResourcesPath = "Resources")
                          .AddDataAnnotationsLocalization()
                          .AddViewLocalization();

            ProjectSettingsModel settingsModel = new ProjectSettingsModel();
            Configuration.GetSection("ProjectSettings").Bind(settingsModel);
            services.AddSingleton(settingsModel);

            EmailSettingsModel emailSettings = new EmailSettingsModel();
            Configuration.GetSection("EmailSettings").Bind(emailSettings);
            services.AddSingleton(emailSettings);
            EmailService emailService = new EmailService(emailSettings);
            services.AddSingleton(emailService);

            //services.AddDbContext<LuckyDuckyMVCContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("LuckyDuckyMVCContextConnection")));

            //services.AddDefaultIdentity<LuckyDuckyMVCUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<LuckyDuckyMVCContext>();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddAuthorization(options => {
                options.AddPolicy("ManageLotteryAccess", policy =>
                          policy.RequireClaim("IsAdminLottery", "true"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseSession();

            app.UseEndpoints(endpoints => {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

            });
        }
    }
}
