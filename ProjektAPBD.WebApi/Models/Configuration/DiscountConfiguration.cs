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

            builder.HasData(
                new Discount() { IdDiscount = 1, DateFrom = DateTime.Now.AddDays(-20), DateTo = DateTime.Now.AddDays(40), IdSoftwareProduct = 1, PercentageValue = 20 },
                new Discount() { IdDiscount = 2, DateFrom = DateTime.Now.AddDays(-10), DateTo = DateTime.Now.AddDays(50), IdSoftwareProduct = 1, PercentageValue = 25 },
                new Discount() { IdDiscount = 3, DateFrom = DateTime.Now.AddDays(-20), DateTo = DateTime.Now.AddDays(-10), IdSoftwareProduct = 2, PercentageValue = 50 },
                new Discount() { IdDiscount = 4, DateFrom = DateTime.Now.AddDays(-1), DateTo = DateTime.Now.AddDays(30), IdSoftwareProduct = 2, PercentageValue = 20 }
            );
        }
    }
}
