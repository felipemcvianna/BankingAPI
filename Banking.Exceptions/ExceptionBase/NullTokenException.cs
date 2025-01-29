namespace Banking.Exceptions.ExceptionBase
{
    public class NullTokenException : ArgumentNullException
    {
        public string ErrorMessage { get; set; }
        public NullTokenException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public NullTokenException()
        {
            
        }
    }
}
