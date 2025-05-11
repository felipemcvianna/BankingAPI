namespace Banking.Exceptions.ExceptionBase;

public class BankingExceptions : Exception, ICustomHttpException
{
    private List<string> Errors { get; set; } = new List<string>();

    public BankingExceptions(string error)
    {
        Errors.Add(error);
    }

    public BankingExceptions(List<string> errors)
    {
        Errors = errors;
    }

    public int StatusCodes => 400;

    object ICustomHttpException.ToResult() => Errors;
}