namespace Banking.Exceptions.ExceptionBase;

<<<<<<< HEAD
public class BusinessException
{
=======
public class BusinessException : BankingExceptions
{
    public IList<string> Erros { get; set; } = new List<string>();

    public BusinessException(string erros)
    {
        Erros.Add(erros);
    }
>>>>>>> 3c5e9f2 (Caso de uso deletar cliente)
    
}