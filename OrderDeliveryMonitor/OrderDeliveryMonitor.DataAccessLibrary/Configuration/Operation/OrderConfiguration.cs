using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderDeliveryMonitor.Model.Operation;

namespace OrderDeliveryMonitor.DataAccessLibrary.Configuration.Operation
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Pedido");

            builder.Property(order => order.OrderId)
                .HasColumnName("Codigo");

            builder.HasKey(order => order.OrderId);

            builder.Property(order => order.OrderNumber)
                .IsRequired()
                    .HasColumnName("Numero");

            builder.Property(order => order.OrderCode)
                .IsRequired()
                    .HasColumnName("MovimentoOperacional_Codigo");

            builder.Property(order => order.Cashier)
                .HasColumnName("Caixa_Codigo");

            builder.Property(order => order.Process)
                .HasColumnName("Processo");

            builder.Property(order => order.Command)
                .HasColumnName("Comando");

            builder.Property(order => order.AwaitingStart)
                .IsRequired(false)
                    .HasColumnName("Aguardo_Inicio");

            builder.Property(order => order.AwaitingEnd)
                .IsRequired(false)
                    .HasColumnName("Aguardo_Fim");

            builder.Property(order => order.PreparingStart)
                    .IsRequired(false)
                        .HasColumnName("Preparo_Inicio");

            builder.Property(order => order.PreparingEnd)
                .IsRequired(false)
                    .HasColumnName("Preparo_Fim");

            builder.Property(order => order.Ready)
                .IsRequired(false)
                    .HasColumnName("Pronto");

            builder.HasMany(order => order.Items)
                .WithOne(orderItem => orderItem.Order)
                    .HasForeignKey(orderItem => orderItem.OrderId);
        }
    }
}
