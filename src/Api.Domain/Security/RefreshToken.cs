using System;

namespace Api.Domain.Security
{
    public class RefreshToken
    {
        public string Username { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Token { get; set; }
    }
}