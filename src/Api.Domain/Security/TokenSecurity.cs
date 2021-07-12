using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using Api.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Api.Domain.Security
{
    public class TokenSecurity
    {
        public static object Create(UserEntity user,
        TokenConfiguration tokenConfiguration,
        SigningConfiguration signingConfiguration)
        {
            if (user == null)
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar."
                };
            }
            else
            {

                var identity = new ClaimsIdentity(
                    new GenericIdentity(user.Email),
                    new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                    });

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(tokenConfiguration.Seconds);

                //create token
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfiguration.Issuer,
                    Audience = tokenConfiguration.Audience,
                    SigningCredentials = signingConfiguration.SigningCredentials,
                    Subject = identity,
                    NotBefore = createDate,
                    Expires = expirationDate
                });

                var token = handler.WriteToken(securityToken);

                //create refresh token
                var _refresToken = CreateRefreshToken(user.Email, expirationDate);

                //succsses object
                return new
                {
                    authenticated = true,
                    created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    acessToken = token,
                    refreshToken = _refresToken.RefreshToken,
                    userName = user.Email,
                    message = "Usuário autenticado."
                };
            }
        }

        private static AccessCredentials CreateRefreshToken(string username, DateTime expirationDate)
        {
            var refreshToken = new AccessCredentials
            {
                Username = username
            };

            string token;
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            refreshToken.RefreshToken = token
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);

            return refreshToken;
        }
    }
}