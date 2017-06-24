using System;
using static OnlineWallet.Core.Domain.ErrorCodes;

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

        private void SetBalance(decimal value)
        {
            if (value < 0)
            {
                throw new DomainException(InvalidBalance, "Cannot set balance to negative");
            }
            else
            {
                Balance = value;
            }
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException(InvalidEmail, "Email cannot be empty.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email.ToLowerInvariant();
        }

        public void SetPhoneNumber(int number)
        {
            if (number == PhoneNumber)
            {
                return;
            }
            else if (number > 999999999)
            {
                throw new DomainException(InvalidPhoneNumber, "Phone number cannot have more than 9 digits");
            }
            else if (number < 0)
            {
                throw new DomainException(InvalidPhoneNumber, "Phone number cannot be negative");
            }

            PhoneNumber = number;
        }

        public void SetAddress(string address)
        {
            if (address == Address)
            {
                return;
            }
            else if (address.Length > 255)
            {
                throw new DomainException(InvalidAddress, "Address is too long.");
            }

            Address = address;
        }

        public bool ValidatePassword(string password)
        {
            return String.Equals(password, Password);
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new DomainException(InvalidPassword,"Password cannot be empty");
            }
            if (password.Length < 6)
            {
                throw new DomainException(InvalidPassword, "Password must contain at least 6 characters.");
            }
            if (password.Length > 32)
            {
                throw new DomainException(InvalidPassword, "Password can not contain more than 32 characters.");
            }
            if (Password == password)
            {
                return;
            }

            Password = password;

        }

        
        public void IncreaseBalance(decimal value)
        {
            if (value <= 0)
            {
                throw new DomainException(InvalidBalance, "Cannot increase with non positive value");
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
                throw new DomainException(InvalidBalance, "Cannot reduce non positive value");
            }
            else if (value > Balance)
            {
                throw new DomainException(InvalidBalance, "Insuficient funds");
            }
            else
            {
                SetBalance(Balance - value);
            }
        }


    }
}
