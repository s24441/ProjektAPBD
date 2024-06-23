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

            builder.HasData(
                new Company() { IdClient = 1, Address = "Koszykowa 24", Email = "evil@corp.com", Phone = "666-333-111", Name = "EvilCorp", KrsNumber = "0000772427" },
                new Company() { IdClient = 2, Address = "Instalatorów 9/24C", Email = "instpol@gmail.com", Phone = "738-091-364", Name = "InstPol", KrsNumber = "0000766811" },
                new Company() { IdClient = 3, Address = "Banacha 50A", Email = "plytex@company.pl", Phone = "(22)824-79-21", Name = "PłyteX", KrsNumber = "0000724592" }
            );
        }
    }
}
