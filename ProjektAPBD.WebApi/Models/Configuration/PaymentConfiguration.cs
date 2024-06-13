using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Models.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(nameof(Payment), Schema.Name);
            builder.HasKey(e => e.IdPayment);
            builder.Property(e => e.IdPayment).UseIdentityColumn();

            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.Value).HasColumnType(ColumnType.Money).IsRequired();

            builder
                .HasOne(e => e.Client)
                .WithMany(e => e.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(e => e.Contract)
                .WithMany(e => e.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
