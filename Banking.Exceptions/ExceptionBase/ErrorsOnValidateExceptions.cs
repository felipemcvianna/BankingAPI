namespace Banking.Exceptions.ExceptionBase;

public class ErrorsOnValidateExceptions : BankingExceptions
{
    public ErrorsOnValidateExceptions(string error) : base(error)
    {
    }

    public ErrorsOnValidateExceptions(List<string> errors) : base(errors)
    {
    }
}