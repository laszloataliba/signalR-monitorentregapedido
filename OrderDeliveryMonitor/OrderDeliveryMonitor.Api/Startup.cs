using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using OrderDeliveryMonitor.Api.Helpers.Swagger;
using OrderDeliveryMonitor.Api.Hubs;
using OrderDeliveryMonitor.ApplicationConfig;
using OrderDeliveryMonitor.Resources;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

namespace OrderDeliveryMonitor.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
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
                version.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(options => { options.SuppressMapClientErrors = true; });

            //Swagger documentation.
            services.AddSwaggerGen(swagger => {
                swagger.ResolveConflictingActions(conflict => conflict.First());

                //Title(v2.0) must be exactly the version number.
                //swagger.SwaggerDoc("v2.0", new Info
                //{
                //    Title = $"{Resource.LBL_APPLICATION_NAME} API - v2.0",
                //    Version = "v2.0",
                //    Description = Resource.LBL_APPLICATION_DOC_DESCRIPTION
                //});

                //Title(v1.0) must be exactly the version number.
                swagger.SwaggerDoc("v1.0", new Info
                {
                    Title = $"{Resource.LBL_APPLICATION_NAME} API - v1.0",
                    Version = "v1.0",
                    Description = Resource.LBL_APPLICATION_DOC_DESCRIPTION
                });

                //Adding xml comments.
                var vApplicationPath = PlatformServices.Default.Application.ApplicationBasePath;

                foreach (var file in Directory.GetFiles(vApplicationPath, "*.xml"))
                    swagger.IncludeXmlComments(file);

                swagger.DocInclusionPredicate((docName, apiDesc) =>
                    {
                        var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                        // would mean this action is unversioned and should be included everywhere
                        if (actionApiVersionModel == null)
                        {
                            return true;
                        }
                        if (actionApiVersionModel.DeclaredApiVersions.Any())
                        {
                            return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                        }
                        return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                    }
                );

                swagger.OperationFilter<ApiVersionOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //CORS.
            app.UseCors("AllowCors");

            //SignalR.
            app.UseSignalR(signalr => {
                signalr.MapHub<OrderDeliveryMonitorHub>($"/{nameof(OrderDeliveryMonitorHub)}");
            });

            app.UseStatusCodePages();
            app.UseMvc();

            //Swagger area.
            app.UseSwagger();
            app.UseSwaggerUI(swaggerUi => {
                swaggerUi.RoutePrefix = string.Empty;
                //swaggerUi.SwaggerEndpoint("/swagger/v2.0/swagger.json", $"{Resource.LBL_VERSION} - v2.0");
                swaggerUi.SwaggerEndpoint("/swagger/v1.0/swagger.json", $"{Resource.LBL_VERSION} - v1.0");
            });
        }
    }
}
