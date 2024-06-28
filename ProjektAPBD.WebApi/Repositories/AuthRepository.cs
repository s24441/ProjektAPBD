using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjektAPBD.WebApi.Configuration;
using ProjektAPBD.WebApi.Extensions;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace ProjektAPBD.WebApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(AuthDbContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
            _context.Database.EnsureCreated();
        }

        public async Task<(string Access, string Refresh)> Login(string username, string password, CancellationToken cancellationToken = default)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == username, cancellationToken);
            var hashed = password.GetHashWithSalt(dbUser?.Salt ?? string.Empty);

            if (hashed != dbUser?.Password)
                throw new SecurityException();

            var claims = new List<Claim> {
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Role, "user")
            };

            if (dbUser.Role == "admin")
                claims.Add(new(ClaimTypes.Role, dbUser.Role));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7232",
                audience: "https://localhost:7232",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[AuthSettings.SecretSection])),
                    SecurityAlgorithms.HmacSha256
                )
            );

            dbUser.RefreshToken = SecurityExtensions.GetRefreshToken();
            dbUser.RefreshTokenExp = DateTime.Now.AddDays(1);

            await _context.SaveChangesAsync();

            return (new JwtSecurityTokenHandler().WriteToken(token), dbUser.RefreshToken);
        }

        public async Task<(string Access, string Refresh)> Refresh(string refreshToken, CancellationToken cancellationToken = default)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, cancellationToken);

            if (dbUser == default)
                throw new SecurityTokenException();
            if (dbUser.RefreshTokenExp < DateTime.Now)
                throw new SecurityTokenException();

            var claims = new List<Claim> {
                new(ClaimTypes.Name, dbUser.Login),
                new(ClaimTypes.Role, "user")
            };

            if (dbUser.Role == "admin")
                claims.Add(new(ClaimTypes.Role, dbUser.Role));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7232",
                audience: "https://localhost:7232",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[AuthSettings.SecretSection])),
                    SecurityAlgorithms.HmacSha256
                )
            );

            dbUser.RefreshToken = SecurityExtensions.GetRefreshToken();
            dbUser.RefreshTokenExp = DateTime.Now.AddDays(1);

            await _context.SaveChangesAsync();

            return (new JwtSecurityTokenHandler().WriteToken(token), dbUser.RefreshToken);
        }
    }
}
