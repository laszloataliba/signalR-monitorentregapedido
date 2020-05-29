using Microsoft.EntityFrameworkCore;
using OrderDeliveryMonitor.DataAccessLibrary.AppConfig;
using OrderDeliveryMonitor.DataAccessLibrary.Configuration.Operation;
using OrderDeliveryMonitor.Model.Operation;

namespace OrderDeliveryMonitor.DataAccessLibrary.Context
{
    public class OrderDeliveryMonitorDataContext : DbContext
    {
        public OrderDeliveryMonitorDataContext()
        {
        }

        public OrderDeliveryMonitorDataContext(DbContextOptions<OrderDeliveryMonitorDataContext> options) : 
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region :: Operation ::

            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());

            #endregion :: Operation ::

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    AppConfiguration.ConnectionString,
                    options => {
                        options.CommandTimeout(AppConfiguration.ConnectionTimeOut);
                        options.EnableRetryOnFailure();
                    }
                );
        }

        #region :: DataSet ::

        #region :: Operation ::

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        #endregion :: Operation ::

        #endregion :: DataSet ::
    }
}
