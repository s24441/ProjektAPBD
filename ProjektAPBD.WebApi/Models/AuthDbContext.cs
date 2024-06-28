using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.Extensions;
using ProjektAPBD.WebApi.Models.Auth;

namespace ProjektAPBD.WebApi.Models
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext() : base() { }
        public AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions) : base(dbContextOptions) { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var (apass, asalt) = "admin1".GetHashAndSalt();
            var (upass, usalt) = "usr123".GetHashAndSalt();
            var tokenExpiration = DateTime.Now.AddDays(1);
            Func<string> getToken = () => SecurityExtensions.GetRefreshToken();

            modelBuilder.Entity<User>().HasData(
                new User { IdUser = 1, Login = "admin", Password = apass, Salt = asalt, Role = "admin", RefreshToken = getToken(), RefreshTokenExp = tokenExpiration },
                new User { IdUser = 2, Login = "user1", Password = upass, Salt = usalt, Role = "user", RefreshToken = getToken(), RefreshTokenExp = tokenExpiration }
            );
        }
    }
}
