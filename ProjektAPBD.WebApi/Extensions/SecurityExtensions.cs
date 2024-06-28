using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjektAPBD.WebApi.Extensions
{
    public static class SecurityExtensions
    {
        public static (string Hash, string Salt) GetHashAndSalt(this string password)
        {
            var saltBuff = GetRandomBytes(16);

            var hash = GetHash(password, saltBuff);

            return (Hash: hash, Salt: Convert.ToBase64String(saltBuff));
        }

        public static string GetHashWithSalt(this string password, string salt) =>
            GetHash(password, Convert.FromBase64String(salt));

        public static string GetRefreshToken() =>
            Convert.ToBase64String(GetRandomBytes(32));



        public static string GetUserIdFromAccessToken(this string accessToken, string secret)
        {
            var tokenValidationParamters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateActor = true,
                ClockSkew = TimeSpan.FromMinutes(2),
                ValidIssuer = "https://localhost:5001", //should come from configuration
                ValidAudience = "https://localhost:5001", //should come from configuration
                ValidateLifetime = false, // We don't validate lifetime
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secret)
                    )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParamters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token!");
            }

            var userId = principal.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new SecurityTokenException($"Missing claim: {ClaimTypes.Name}!");
            }

            return userId;
        }

        private static byte[] GetRandomBytes(int size)
        {
            var randBuff = new byte[size];
            using var rand = RandomNumberGenerator.Create();
            rand.GetBytes(randBuff);

            return randBuff;
        }

        private static string GetHash(string password, byte[] saltBuff) =>
        Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBuff,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 15000,
                numBytesRequested: 32
            ));
    }
}
