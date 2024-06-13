using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Models.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(nameof(Company), Schema.Name);

            builder.HasIndex(e => e.KrsNumber).IsUnique();

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.KrsNumber).HasMaxLength(20).IsRequired();
        }
    }
}
