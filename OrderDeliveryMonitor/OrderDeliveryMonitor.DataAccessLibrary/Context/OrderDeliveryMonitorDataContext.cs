using Microsoft.EntityFrameworkCore;
using OrderDeliveryMonitor.DataAccessLibrary.AppConfig;

namespace OrderDeliveryMonitor.DataAccessLibrary.Context
{
    public class OrderDeliveryMonitorDataContext : DbContext
    {
        public OrderDeliveryMonitorDataContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                AppConfiguration.ConnectionString,
                connection => connection.CommandTimeout(AppConfiguration.ConnectionTimeOut)
            );
        }
    }
}
