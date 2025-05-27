namespace Banking.Exceptions.ExceptionBase
{
    public class NullTokenException : BankingExceptions
    {
        public NullTokenException(string error) : base(error)
        {
        }

        public NullTokenException(List<string> errors) : base(errors)
        {
        }
    }
}