namespace Banking.Exceptions.ExceptionBase;

public class DepositoException : BankingExceptions
{
    public DepositoException(string error) : base(error)
    {
    }

    public DepositoException(List<string> errors) : base(errors)
    {
    }
}