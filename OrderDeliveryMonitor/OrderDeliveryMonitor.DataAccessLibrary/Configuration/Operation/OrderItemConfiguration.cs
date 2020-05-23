using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Resources;

namespace OrderDeliveryMonitor.DataAccessLibrary.Configuration.Operation
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(Resource.TBL_ORDER_ITEM);

            builder.Property(orderItem => orderItem.OrderItemId)
                .HasColumnName(Resource.CLM_CODE);

            builder.HasKey(orderItem => orderItem.OrderItemId);

            builder.Property(orderItem => orderItem.OrderId)
                .HasColumnName(Resource.CLM_ORDER_CODE);

            builder.Property(orderItem => orderItem.Product)
                .HasMaxLength(60)
                    .HasColumnName(Resource.CLM_PRODUCT_NAME);

            builder.Property(orderItem => orderItem.Quantity)
                .HasColumnName(Resource.CLM_QUANTITY);

            builder.Ignore(orderItem => orderItem.Order);
        }
    }
}
