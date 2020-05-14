using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderDeliveryMonitor.Model.Operation;

namespace OrderDeliveryMonitor.DataAccessLibrary.Configuration.Operation
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("PedidoItem");

            builder.Property(orderItem => orderItem.OrderItemId)
                .HasColumnName("Codigo");

            builder.HasKey(orderItem => orderItem.OrderItemId);

            builder.Property(orderItem => orderItem.OrderId)
                .HasColumnName("Pedido_Codigo");

            builder.Property(orderItem => orderItem.Product)
                .HasMaxLength(60)
                    .HasColumnName("Produto_Nome");

            builder.Property(orderItem => orderItem.Quantity)
                .HasColumnName("Quantidade");
        }
    }
}
