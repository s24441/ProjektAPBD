using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Models.Entities.Abstracts;

namespace ProjektAPBD.WebApi.Models.Configuration
{
    public class ContractBaseConfiguration : IEntityTypeConfiguration<ContractBase>
    {
        public void Configure(EntityTypeBuilder<ContractBase> builder)
        {
            builder.UseTpcMappingStrategy();

            builder.HasKey(e => e.IdContract);

            builder.Property(e => e.Price).HasColumnType(ColumnType.Money).IsRequired();

            builder
                .HasOne(e => e.Client)
                .WithMany(e => e.Contracts)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasMany(e => e.Payments)
                .WithOne(e => e.Contract)
                .HasForeignKey(e => e.IdContract);
        }
    }
}
