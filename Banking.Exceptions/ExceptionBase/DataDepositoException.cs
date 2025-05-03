namespace Banking.Exceptions.ExceptionBase;

public class DataDepositoException : BankingExceptions
{
    public DataDepositoException(string message) : base(message)
    {
    }

    public DataDepositoException(List<string> errors) : base(errors)
    {
    }
}