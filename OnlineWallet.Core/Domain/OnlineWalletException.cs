using System;

namespace OnlineWallet.Core.Domain
{
    public class OnlineWalletException : Exception
    {
        public string Code { get; }

        protected OnlineWalletException()
        {
        }

        protected OnlineWalletException(string code)
        {
            Code = code;
        }

        protected OnlineWalletException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected OnlineWalletException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected OnlineWalletException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected OnlineWalletException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}