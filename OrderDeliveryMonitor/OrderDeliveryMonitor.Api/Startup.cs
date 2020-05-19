using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OrderDeliveryMonitor.Api.Hubs;
using OrderDeliveryMonitor.ApplicationConfig;

namespace OrderDeliveryMonitor.Api
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
            services.AddCors(options => {
                options.AddPolicy("AllowCors",
                    builder => builder
                            .WithOrigins($"{Configuration.GetValue<string>("WebApplicationServerPath")}")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                        );
            });

            services.AddSignalR(cfg =>
                cfg.EnableDetailedErrors = true
            ).AddJsonProtocol(options => 
                options.PayloadSerializerSettings.Converters.Add(new StringEnumConverter(true))
            );

            AppConfig.ConfigureWebApi(services);

            services.AddMvc()
                .AddJsonOptions(options => 
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors("AllowCors");

            app.UseSignalR(signalr => {
                signalr.MapHub<OrderDeliveryMonitorHub>($"/{nameof(OrderDeliveryMonitorHub)}");
            });

            app.UseMvc();
        }
    }
}
