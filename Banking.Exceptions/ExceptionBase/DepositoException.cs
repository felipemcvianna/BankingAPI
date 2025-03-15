namespace Banking.Exceptions.ExceptionBase;

public class DepositoException : BankingExceptions
{
    public DepositoException(List<string> errors)
    {
        Errors = errors;
    }

    public DepositoException(string error)
    {
        Errors = new List<string> { error };
    }

    public List<string> Errors { get; set; }
}