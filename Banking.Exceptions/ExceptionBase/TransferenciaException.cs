namespace Banking.Exceptions.ExceptionBase;

public class TransferenciaException : BankingExceptions
{
    public TransferenciaException(string error) : base(error)
    {
    }

    public TransferenciaException(List<string> errors) : base(errors)
    {
    }
}