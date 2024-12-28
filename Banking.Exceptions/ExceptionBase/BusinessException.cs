namespace Banking.Exceptions.ExceptionBase;

public class BusinessException : BankingExceptions
{
    public IList<string> Erros { get; set; } = new List<string>();

    public BusinessException(string erros)
    {
        Erros.Add(erros);
    }
    public BusinessException(List<string> erros)
    {
        Erros = erros;
    }

}