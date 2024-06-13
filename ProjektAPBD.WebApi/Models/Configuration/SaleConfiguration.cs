using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Models.Configuration
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable(nameof(Sale), Schema.Name);

            builder.Property(e => e.CreationDate).IsRequired();
            builder.Property(e => e.ExpirationDate).IsRequired();
            builder.Property(e => e.SupportYearsAmount).IsRequired();
        }
    }
}
