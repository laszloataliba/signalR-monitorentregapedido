using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Resources;

namespace OrderDeliveryMonitor.DataAccessLibrary.Configuration.Operation
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(Resource.TBL_ORDER);

            builder.Property(order => order.OrderId)
                .HasColumnName(Resource.CLM_CODE);

            builder.HasKey(order => order.OrderId);

            builder.Property(order => order.OrderNumber)
                    .HasColumnName(Resource.CLM_ORDER_NUMBER);

            builder.Property(order => order.OrderCode)
                .IsRequired()
                    .HasColumnName(Resource.CLM_ORDER_IDENTIFICATION);

            builder.Property(order => order.Cashier)
                .HasColumnName(Resource.CLM_CASHIER_CODE);

            builder.Property(order => order.Process)
                .HasColumnName(Resource.CLM_PROCESS);

            builder.Property(order => order.Command)
                .HasColumnName(Resource.CLM_COMMAND);

            builder.Property(order => order.AwaitingStart)
                .IsRequired(false)
                    .HasColumnName(Resource.CLM_AWAITING_START);

            builder.Property(order => order.AwaitingEnd)
                .IsRequired(false)
                    .HasColumnName(Resource.CLM_AWAITING_END);

            builder.Property(order => order.PreparingStart)
                    .IsRequired(false)
                        .HasColumnName(Resource.CLM_PREPARING_START);

            builder.Property(order => order.PreparingEnd)
                .IsRequired(false)
                    .HasColumnName(Resource.CLM_PREPARING_END);

            builder.Property(order => order.ReadyStart)
                .IsRequired(false)
                    .HasColumnName(Resource.CLM_READY_START);

            builder.Property(order => order.ReadyEnd)
                .IsRequired(false)
                    .HasColumnName(Resource.CLM_READY_END);

            builder.Property(order => order.RedeemDate)
                .IsRequired(false)
                    .HasColumnName(Resource.CLM_REDEEM_DATE);

            builder.HasMany(order => order.Items)
                .WithOne(orderItem => orderItem.Order)
                    .HasForeignKey(orderItem => orderItem.OrderId);
        }
    }
}
