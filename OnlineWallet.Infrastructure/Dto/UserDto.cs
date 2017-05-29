using System;
using OnlineWallet.Core.Domain;

namespace OnlineWallet.Infrastructure.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public int? PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Address { get; set; }

        public Account Account { get; set; }
        public decimal Balance { get; set; }
    }
}