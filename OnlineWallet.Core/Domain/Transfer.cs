using static OnlineWallet.Core.Domain.ErrorCodes;

namespace OnlineWallet.Core.Domain

{
    public class Transfer : Transaction
    {
        protected Transfer()
        {
        }
        public Transfer(decimal amount, User userFrom, User userTo) : base(amount, userFrom)
        {
            UserTo = SetUserTo(userTo);
        }

        public User UserTo { get; protected set; }

        private User SetUserTo(User userTo)
        {
            if (UserFrom.Id == userTo.Id)
            {
                throw new DomainException(InvalidUserTo,"Invalid user");
            }
            return userTo;
        }
    }
}