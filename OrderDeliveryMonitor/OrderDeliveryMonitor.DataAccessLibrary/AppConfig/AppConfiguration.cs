using Microsoft.Extensions.Configuration;
using System.IO;

namespace OrderDeliveryMonitor.DataAccessLibrary.AppConfig
{
    public class AppConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        private static string _connectionTimeOut;

        /// <summary>
        /// 
        /// </summary>
        public AppConfiguration()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        static AppConfiguration()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionString => GetConnectionString();

        /// <summary>
        /// 
        /// </summary>
        public static int ConnectionTimeOut => int.Parse(_connectionTimeOut);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString()
        {
            var vConfigBuilder = new ConfigurationBuilder();

            var vPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            vConfigBuilder.AddJsonFile(vPath, false);

            var vRoot = vConfigBuilder.Build();

            var vConnectionString = vRoot.GetSection("ConnectionStrings").GetSection("DBSource").Value;
            _connectionTimeOut = vRoot.GetSection("ConnectionStrings").GetSection("ConnectionTimeOut").Value;

            return vConnectionString;
        }
    }
}
