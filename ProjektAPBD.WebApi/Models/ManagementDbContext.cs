using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.Models.Entities;
using ProjektAPBD.WebApi.Models.Entities.Abstracts;
using System.Reflection;

namespace ProjektAPBD.WebApi.Models
{
    public class ManagementDbContext : DbContext
    {
        public ManagementDbContext() { }

        public ManagementDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<ClientBase> Clients { get; set; }
        public virtual DbSet<PhysicalPerson> PersonClients { get; set; }
        public virtual DbSet<Company> CompanyClients { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<ContractBase> Contracts { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<SoftwareProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
