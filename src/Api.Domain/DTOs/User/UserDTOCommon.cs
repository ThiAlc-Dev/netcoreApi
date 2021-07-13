using System;

namespace Api.Domain.DTOs.User
{
    public class UserDTOCommon
    {
        public Guid ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}