using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BotDetect.Web;
using BackSiteTemplate.DB;
using BackSiteTemplate.Interface;
using BackSiteTemplate.MiddleWare;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static BackSiteTemplate.Interface.AccountServices;
using static BackSiteTemplate.Interface.IdentityServices;
using static BackSiteTemplate.Interface.ProductionHtmlServices;
using static BackSiteTemplate.Interface.RoleGroupServices;
using static BackSiteTemplate.Interface.WhiteListServices;

namespace BackSiteTemplate
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

            #region DB Setting。
            //Update Table
            //Scaffold - DbContext "Data Source=...Initial Catalog=BackSiteTemplate;Integrated Security=False;User ID=sa;Password=1111;" Microsoft.EntityFrameworkCore.SqlServer - t AccountList - f - OutputDir DB
            services.AddDbContext<BackSiteDBTempContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString("DB")));
            #endregion

            #region System Cookie Setting
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/";
                    options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Cookie.HttpOnly = true;
                    //登入時間長度
                    //options.ExpireTimeSpan = TimeSpan.FromSeconds(1);
                    //未授權使用者導向路徑
                    options.LoginPath = "/Home/Index";
                    //使用者登出後導向路徑
                    options.LogoutPath = "/";

                    // ReturnUrlParameter requires `using Microsoft.AspNetCore.Authentication.Cookies;`
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                    options.SlidingExpiration = true;
                }
            );
            #endregion

            //帳號管理
            services.AddScoped<IAccountAction, AccountService>();
            services.AddScoped<IIdentityAction, IdentityService>();

            //頁面HTML產生
            services.AddScoped<IProductionHtmlAction, ProductionHtmlService>();

            //權限群組
            services.AddScoped<IRoleGroupAction, RoleGroupService>();

            //白名單
            services.AddScoped<IWhiteListAction, WhiteListService>();


            services.AddMvc();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //自訂Error StatusCode >= 400時的錯誤導向(AJAX)
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode >= 400)
                {
                    context.Request.Path = "/Error/Index";
                    context.Response.StatusCode = 200;

                    await next();
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
           
        }
    }
}
