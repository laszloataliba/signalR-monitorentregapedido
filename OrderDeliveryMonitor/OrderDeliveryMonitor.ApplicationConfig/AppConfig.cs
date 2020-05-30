using Microsoft.Extensions.DependencyInjection;
using OrderDeliveryMonitor.Facade.Implementation.Operation;
using OrderDeliveryMonitor.Facade.Interface.Operation;

namespace OrderDeliveryMonitor.ApplicationConfig
{
    public class AppConfig
    {
        public static void ConfigureWebApplication(IServiceCollection pServices)
        {
            AppConfig.Configure(pServices);
        }

        public static void ConfigureWebApi(IServiceCollection pServices)
        {
            AppConfig.Configure(pServices);
        }

        private static void Configure(IServiceCollection pServices)
        {
            pServices.AddScoped(typeof(IFOrder), typeof(FOrder));
        }
    }
}
