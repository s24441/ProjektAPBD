using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Models.Configuration
{
    public class PhysicalPersonConfiguration : IEntityTypeConfiguration<PhysicalPerson>
    {
        public void Configure(EntityTypeBuilder<PhysicalPerson> builder)
        {
            builder.ToTable(nameof(PhysicalPerson), Schema.Name);

            builder.HasIndex(e => e.Pesel).IsUnique();

            builder.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Pesel).HasMaxLength(20).IsRequired();

            builder.HasData(
                new PhysicalPerson() { IdClient = 4, Address = "Duracza 76/21", Email = "amichalecki@gmail.com", Phone = "726-487-621", Pesel = "89050729992", FirstName = "Andrzej", LastName = "Michalecki" },
                new PhysicalPerson() { IdClient = 5, Address = "Duracza 76/21", Email = "asia94@gmail.com", Phone = "669-910-436", Pesel = "94070507007", FirstName = "Joanna", LastName = "Michalecka" },
                new PhysicalPerson() { IdClient = 6, Address = "Partyzantki 12C", Email = "plytex@company.pl", Phone = "(+48)842-279-216", Pesel = "08280732893", FirstName = "Anna", LastName = "Łącka" }
            );
        }
    }
}
