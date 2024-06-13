using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Models.Configuration
{
    public class SoftwareProductConfiguration : IEntityTypeConfiguration<SoftwareProduct>
    {
        public void Configure(EntityTypeBuilder<SoftwareProduct> builder)
        {
            builder.ToTable(nameof(SoftwareProduct), Schema.Name);

            builder.HasKey(e => e.IdSoftwareProduct);
            builder.Property(e => e.IdSoftwareProduct).UseIdentityColumn();

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(200).IsRequired();
            builder.Property(e => e.ActualVersion).HasMaxLength(50).IsRequired();
            builder.Property(e => e.ActualVersionReleaseDate).IsRequired();
            builder.Property(e => e.Category).HasMaxLength(20).IsRequired();

            builder
                .HasMany(e => e.Discounts)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.IdSoftwareProduct);
            
            builder
                .HasMany(e => e.Contracts)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.IdSoftwareProduct);
        }
    }
}
