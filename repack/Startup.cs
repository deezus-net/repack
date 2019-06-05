using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using repack.Classes;
using repack.Entities;

namespace repack
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                op => { op.LoginPath = "/"; });

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = Configuration.GetValue<string>("RepackDbHost"),
                Port = Configuration.GetValue<int>("RepackDbPort"),
                Username = Configuration.GetValue<string>("RepackDbUser"),
                Password = Configuration.GetValue<string>("RepackDbPassword"),
                Database = Configuration.GetValue<string>("RepackDbName")
            };
            var connectionString = builder.ToString();

            services.AddDbContext<Db>(op => { op.UseNpgsql(connectionString); });
            services.AddHttpClient();

            // di
            services.AddSingleton(new AppSetting()
            {
                Salt = Configuration.GetValue<string>("RepackSalt"),
                AdminId = Configuration.GetValue<string>("RepackAdminId"),
                AdminPassword = Configuration.GetValue<string>("RepackAdminPassword")
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
               /* .AddDataAnnotationsLocalization(options =>
                {
                    // DataAnnotation を使ったときのメッセージは SharedResource に集約する
                    options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResource));
                })*/;
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
                new CultureInfo("ja"),
                new CultureInfo("en-US")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}