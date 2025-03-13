namespace Banking.Exceptions.ExceptionBase;

public class ErrorsOnValidateExceptions : BankingExceptions
{
    public List<string> Erros { get; set; } = new();

    public ErrorsOnValidateExceptions(List<string> errors)
    {
        Erros = errors;
    }

    public ErrorsOnValidateExceptions(string erro)
    {
        Erros = new List<string>()
        {
            erro
        };
    }
}