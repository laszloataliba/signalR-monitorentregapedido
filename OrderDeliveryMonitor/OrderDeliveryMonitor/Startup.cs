using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OrderDeliveryMonitor.ApplicationConfig;
using OrderDeliveryMonitor.Services.Operation.Implementation;
using OrderDeliveryMonitor.Services.Operation.Interface;
using OrderDeliveryMonitor.Utility;
using System;

namespace OrderDeliveryMonitor
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

            AppConfig.ConfigureWebApplication(services);
            
            //Order services
            services.AddHttpClient<IOrderService, OrderService>(order => {
                order.BaseAddress = new Uri($"{Utilities.WEB_API_SERVER_PATH}/api/v1/Operation/Orders");
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes
                    .MapAreaRoute(
                        name: $"{nameof(Areas.Security).ToLower()}_default",
                        areaName: $"{nameof(Areas.Security)}",
                        template: $"{nameof(Areas.Security)}/{{controller=Login}}/{{action=Index}}/{{id?}}")

                    .MapAreaRoute(
                        name: $"{nameof(Areas.Cockpit).ToLower()}_default",
                        areaName: $"{nameof(Areas.Cockpit)}",
                        template: $"{nameof(Areas.Cockpit)}/{{controller=Dashboard}}/{{action=Index}}/{{id?}}")

                    .MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
