using Microsoft.EntityFrameworkCore;
using OrderDeliveryMonitor.DataAccessLibrary.AppConfig;
using OrderDeliveryMonitor.Model.Operation;

namespace OrderDeliveryMonitor.DataAccessLibrary.Context
{
    public class OrderDeliveryMonitorDataContext : DbContext
    {
        public OrderDeliveryMonitorDataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                AppConfiguration.ConnectionString,
                connection => connection.CommandTimeout(AppConfiguration.ConnectionTimeOut)
            );
        }

        //public OrderDeliveryMonitorDataContext(DbContextOptions<OrderDeliveryMonitorDataContext> options) : 
        //    base(options)
        //{
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        AppConfiguration.ConnectionString,
        //        connection => connection.CommandTimeout(AppConfiguration.ConnectionTimeOut)
        //    );
        //}

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
