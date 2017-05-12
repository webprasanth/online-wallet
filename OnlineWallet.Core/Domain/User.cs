using System;
using System.IO.Compression;

namespace OnlineWallet.Core.Domain
{
    public class User
    {
        protected User()
        {
        }

        public User(string email, string password, string fullName, int phoneNumber = 0, string address = "")
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Password = password;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
            Address = address;
            Account = Account.NewAccount();
        }

        public Guid Id { get; protected set; }

        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string FullName { get; protected set; }

        public int PhoneNumber { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public string Address { get; protected set; }

        public Account Account { get; protected set; }


        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email cannot be empty.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email.ToLowerInvariant();
        }

        public bool ValidatePassword(string password)
        {
            return String.Equals(password, Password);
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("Password cannot be empty");
            }
            if (password.Length < 6)
            {
                throw new Exception("Password must contain at least 6 characters.");
            }
            if (password.Length > 32)
            {
                throw new Exception("Password can not contain more than 32 characters.");
            }
            if (Password == password)
            {
                return;
            }

            Password = password;

        }
    }
}
