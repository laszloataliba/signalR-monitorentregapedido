using Microsoft.Extensions.Configuration;
using System.IO;

namespace OrderDeliveryMonitor.Utility
{
    public static class Utilities
    {
        public const string RELOAD_AWAITING_CONTAINER = "ReloadAwaitingContainer";        
        public const string LOAD_AWAITING_CONTAINER = "LoadAwaitingContainer";
        public const string LOAD_AWAITING_CONTAINER_FOR_CUSTOMERS = "LoadAwaitingContainerForCustomers";
        public const string LOAD_PREPARING_CONTAINER = "LoadPreparingContainer";
        public const string LOAD_PREPARING_CONTAINER_FOR_CUSTOMERS = "LoadPreparingContainerForCustomers";
        public const string LOAD_READY_CONTAINER = "LoadReadyContainer";
        public const string LOAD_READY_CONTAINER_FOR_CUSTOMER = "LoadReadyContainerForCustomers";

        public const string RELOAD_AWAITING_CONTAINER_METHOD = "ReloadAwaitingContainer";
        public const string FROM_AWAITING_TO_PREPARING_METHOD = "FromAwaitingToPreparing";
        public const string FROM_PREPARING_TO_READY_METHOD = "FromPreparingToReady";
        public const string HIDE_READY_ORDER_BY_TIMEOUT_METHOD = "HideReadyOrderByTimeOut";

        public const string AWAITING_CONTAINER = "AwaitingContainer";
        public const string PREPARING_CONTAINER = "PreparingContainer";
        public const string READY_CONTAINER = "ReadyContainer";

        public const string AWAITING_CLASS = "awaiting";
        public const string PREPARING_CLASS = "preparing";
        public const string READY_CLASS = "ready";
        public const string REDEEMED_CLASS = "ready";

        public static string HUB_SERVER_PATH = HubServerPath();
        public static string WEB_API_SERVER_PATH = WebApiServerPath();

        public static string ORDER_SERVICE_VERSION = OrderServiceVersion();
        public static string USER_SERVICE_VERSION = UserServiceVersion();

        private static string HubServerPath()
        {
            var vConfigBuilder = new ConfigurationBuilder();

            var vPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            vConfigBuilder.AddJsonFile(vPath, false);

            var vRoot = vConfigBuilder.Build();

            var vHubPath = vRoot.GetSection("HubServerPath").Value;

            return vHubPath;
        }

        private static string WebApiServerPath()
        {
            var vConfigBuilder = new ConfigurationBuilder();

            var vPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            vConfigBuilder.AddJsonFile(vPath, false);

            var vRoot = vConfigBuilder.Build();

            var vServicePath = vRoot.GetSection("WebAPIServerPath").Value;

            return vServicePath;
        }

        private static string OrderServiceVersion()
        {
            var vConfigBuilder = new ConfigurationBuilder();

            var vPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            vConfigBuilder.AddJsonFile(vPath, false);

            var vRoot = vConfigBuilder.Build();

            var vOrderServiceVersion = vRoot.GetSection("OrderServiceVersion").Value;

            return vOrderServiceVersion;
        }

        private static string UserServiceVersion()
        {
            var vConfigBuilder = new ConfigurationBuilder();

            var vPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            vConfigBuilder.AddJsonFile(vPath, false);

            var vRoot = vConfigBuilder.Build();

            var vUserServiceVersion = vRoot.GetSection("UserServiceVersion").Value;

            return vUserServiceVersion;
        }
    }
}
