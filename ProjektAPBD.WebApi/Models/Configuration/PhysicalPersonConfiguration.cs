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
        }
    }
}
