namespace Banking.Exceptions.ExceptionBase;

public class ErrorsOnValidateExceptions : BankingExceptions
{
    public List<string> Erros { get; set; }

    public ErrorsOnValidateExceptions(List<string> errors)
    {
        Erros = errors;
    }

    public void PreencherErrors(string erro)
    {
        Erros = new List<string>()
        {
            erro
        };
    }
}