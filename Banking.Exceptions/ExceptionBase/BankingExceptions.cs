namespace Banking.Exceptions.ExceptionBase;

public class BankingExceptions : SystemException
{
    public List<string> Errors { get; set; }

    public BankingExceptions(string error)
    {
        Errors = new List<string>()
        {
            error
        };
    }

    public BankingExceptions(List<string> errors)
    {
        Errors = errors;
    }

    public BankingExceptions()
    {
    }
}