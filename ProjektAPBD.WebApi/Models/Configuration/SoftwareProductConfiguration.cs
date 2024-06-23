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

            builder.HasData(
                new SoftwareProduct() { IdSoftwareProduct = 1, Name = "SuperSoft", Category = "Rozrywka", Description = "Super aplikacja do rozrywki", ActualVersion = "2.1", ActualVersionReleaseDate = DateTime.Now.AddDays(-100) },
                new SoftwareProduct() { IdSoftwareProduct = 2, Name = "GamexPro", Category = "Design gier", Description = "Interaktywny progam do tworenia gier", ActualVersion = "7.8", ActualVersionReleaseDate = DateTime.Now.AddDays(-20) },
                new SoftwareProduct() { IdSoftwareProduct = 3, Name = "Alerter", Category = "Rozwój osobisty", Description = "Inteligentny organizer, ktory zawsze przypomni o najważniejszych wydarzeniach", ActualVersion = "1.0", ActualVersionReleaseDate = DateTime.Now.AddDays(-65) }
            );
        }
    }
}
