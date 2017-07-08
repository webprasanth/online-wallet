namespace OnlineWallet.Core.Domain
{
    public static class ErrorCodes
    {
        public static string InvalidEmail => "invalid_email";
        public static string InvalidPassword => "invalid_password";
        public static string InvalidBalance => "invalid_balance";
        public static string InvalidPhoneNumber => "invalid_phoneNumber";
        public static string InvalidAddress => "invalid_address";
        public static string InvalidUserTo => "invalid_user";
        public static string InvalidTransactionsAmount => "invalid_transaction's_amount";
    }
}