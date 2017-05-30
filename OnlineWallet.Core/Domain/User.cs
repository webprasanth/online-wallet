using System;

namespace OnlineWallet.Core.Domain
{
    public class User
    {
        protected User()
        {
        }

        public User(string email, string password, string fullName, int? phoneNumber = null, string address = "")
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Password = password;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
            Address = address;
            SetBalance(0);
        }

        public Guid Id { get; protected set; }

        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string FullName { get; protected set; }

        public int? PhoneNumber { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public string Address { get; protected set; }

        public decimal Balance { get; protected set; }


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

        private void SetBalance(decimal value)
        {
            if (value < 0)
            {
                throw new InvalidOperationException("Cannot set balance to negative");
            }
            else
            {
                Balance = value;
            }
        }

        public void IncreaseBalance(decimal value)
        {
            if (value <= 0)
            {
                throw new InvalidOperationException("Cannot increase with non positive value");
            }
            else
            {
                SetBalance(Balance+value);
            }
        }

        public void ReduceBalance(decimal value)
        {
            if (value <= 0)
            {
                throw new InvalidOperationException("Cannot reduce non positive value");
            }
            else if (value > Balance)
            {
                throw new InvalidOperationException("Insuficient funds");
            }
            else
            {
                SetBalance(Balance - value);
            }
        }


    }
}
