using System;
using System.Collections.Generic;
using System.Linq;
using OnlineWallet.Core.Domain;
using OnlineWallet.Core.Repositories;

namespace OnlineWallet.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>
        {
                new User("user1@aol.com","password1","Full Name1",236547896,"Some Address"),
                new User("user2@aol.com","password2","Full Name2",1896,"S Address")

        };

        public User Get(Guid id)
        {
            return _users.SingleOrDefault(x => x.Id == id);
        }

        public User Get(string email)
        {
            return _users.SingleOrDefault(x => x.Email.ToLowerInvariant() == email);
        }

        public IEnumerable<User> GetAll()
            => _users;

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Update(User user)
        {
            //TO DO
        }

        public void Remove(Guid id)
        {
            var user = Get(id);
            _users.Remove(user);
        }
    }
}