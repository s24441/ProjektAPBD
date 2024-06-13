using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Models.Configuration
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable(nameof(Discount), Schema.Name);
            builder.HasKey(e => e.IdDiscount);
            builder.Property(e => e.IdDiscount).UseIdentityColumn();

            builder.Property(e => e.PercentageValue).IsRequired();
            builder.Property(e => e.DateFrom).IsRequired();
            builder.Property(e => e.DateTo).IsRequired();

            builder
                .HasOne(e => e.Product)
                .WithMany(e => e.Discounts);
        }
    }
}
