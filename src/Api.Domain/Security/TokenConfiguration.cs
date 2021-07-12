using System;

namespace Api.Domain.Security
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }

    public class AccessCredentials
    {
        public string Username { get; set; }
        public string RefreshToken { get; set; }
        public string GrantType { get; set; }
    }
}