using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.Models.Configuration.Consts;
using ProjektAPBD.WebApi.Models.Entities;

namespace ProjektAPBD.WebApi.Models.Configuration
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable(nameof(Subscription), Schema.Name);

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.RenewalPeriod).IsRequired();
            builder.Property(e => e.EndTime).IsRequired();
        }
    }
}
