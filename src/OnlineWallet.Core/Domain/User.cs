using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWallet.Core.Domain
{
    public class User
    {
        public User(string email, string password, string fullName, int phoneNumber, string address)
        {
            Id = Guid.NewGuid();
            Email = email;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
            Address = address;
            Account = new Account();
        }

        public Guid Id { get; protected set; }

        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string FullName { get; protected set; }

        public int PhoneNumber { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public string Address { get; protected set; }

        public Account Account { get; protected set; }


    }
}
