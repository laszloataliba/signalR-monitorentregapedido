using Microsoft.Extensions.DependencyInjection;

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

        }
    }
}
