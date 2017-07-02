using System;
using System.Text.RegularExpressions;
using static OnlineWallet.Core.Domain.ErrorCodes;

namespace OnlineWallet.Core.Domain
{
    public class User
    {
        private static readonly Regex EmailRegex = new Regex("^\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

        protected User()
        {
        }

        public User(string email, string password, string fullName, int? phoneNumber = null, string address = "")
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            CreatedAt = DateTime.UtcNow;
            SetPhoneNumber(phoneNumber);
            SetAddress(address);
            SetEmail(email);
            SetPassword(password);
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
            if (Email == email)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException(InvalidEmail, "Email cannot be empty.");
            }
            if (!EmailRegex.IsMatch(email))
            {
                throw new DomainException(InvalidEmail, "Invalid email");
            }

            Email = email.ToLowerInvariant();
        }

        public void SetPhoneNumber(int? number)
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

        /// <summary>
        /// Compare password to the one that is currently set.
        /// Returns true whenever passwords are equal.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidatePassword(string password)
        {
            return String.Equals(password, Password);
        }

        public void SetPassword(string password)
        {
            if (Password == password)
            {
                return;
            }
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
