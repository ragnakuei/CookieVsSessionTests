using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore
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
            services.AddControllersWithViews();

            services.AddHttpContextAccessor();

            services.AddSession(options =>
                                {
                                    // 手動指定 Session Cookie
                                    options.Cookie = new CookieBuilder
                                                     {
                                                         Name         = "TestSession",
                                                         // Path         = null,
                                                         // Domain       = null,
                                                         HttpOnly     = true,
                                                         SameSite     = SameSiteMode.Strict,
                                                         SecurePolicy = CookieSecurePolicy.SameAsRequest,
                                                         // Expiration   = TimeSpan.FromSeconds(5),
                                                         Expiration   = TimeSpan.FromHours(1),
                                                         // MaxAge       = null,
                                                         IsEssential  = false,
                                                     };
                                    options.IdleTimeout = TimeSpan.FromHours(1);
                                });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                               {
                                   options.LoginPath          = "/Account/Login";
                                   options.LogoutPath         = "/Account/Logout";
                                   options.AccessDeniedPath   = "/Account/AccessDenied";
                                   options.ReturnUrlParameter = "returnUrl";
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                             {
                                 endpoints.MapControllerRoute(
                                                              name: "default",
                                                              pattern: "{controller=Home}/{action=Index}/{id?}");
                             });
        }
    }
}
