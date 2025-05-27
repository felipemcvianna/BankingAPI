namespace Banking.Exceptions.ExceptionBase;

public class BusinessException : BankingExceptions
{
    public BusinessException(string error) : base(error)
    {
    }

    public BusinessException(List<string> errors) : base(errors)
    {
    }
}