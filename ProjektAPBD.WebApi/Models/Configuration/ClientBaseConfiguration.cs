using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjektAPBD.WebApi.Models.Entities.Abstracts;

namespace ProjektAPBD.WebApi.Models.Configuration
{
    public class ClientBaseConfiguration : IEntityTypeConfiguration<ClientBase>
    {
        public void Configure(EntityTypeBuilder<ClientBase> builder)
        {
            builder.UseTpcMappingStrategy();

            builder.HasKey(e => e.IdClient);

            builder.Property(e => e.Address).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Phone).HasMaxLength(100);

            builder
                .HasMany(e => e.Contracts)
                .WithOne(e => e.Client)
                .HasForeignKey(e => e.IdClient);

            builder
                .HasMany(e => e.Payments)
                .WithOne(e => e.Client)
                .HasForeignKey(e => e.IdClient);
        }
    }
}
