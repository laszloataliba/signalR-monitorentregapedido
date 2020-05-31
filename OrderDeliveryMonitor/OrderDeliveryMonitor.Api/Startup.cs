using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            //Adding CORS middleware.
            services.AddCors(options => {
                options.AddPolicy("AllowCors",
                    builder => builder
                            .WithOrigins($"{Configuration.GetValue<string>("WebApplicationServerPath")}")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                        );
            });

            //Adding signalR middleware.
            services.AddSignalR(cfg => cfg.EnableDetailedErrors = true);

            //Dependency injection.
            AppConfig.ConfigureWebApi(services);

            //API versioning middleware.
            services.AddApiVersioning(version => {
                version.ReportApiVersions = true;
                version.AssumeDefaultVersionWhenUnspecified = true;
                //version.DefaultApiVersion = new ApiVersion(1, 0);
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
            
            app.UseCors("AllowCors");

            app.UseSignalR(signalr => {
                signalr.MapHub<OrderDeliveryMonitorHub>($"/{nameof(OrderDeliveryMonitorHub)}");
            });

            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
