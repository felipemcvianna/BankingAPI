namespace Banking.Exceptions.ExceptionBase.Deposito;

public class DataDepositoException : BankingExceptions
{
    public DataDepositoException(List<string> errors)
    {
        Errors = errors;
    }
    
    public DataDepositoException(string error)
    {
        Errors = new List<string> { error };
    }

    public List<string> Errors { get; set; }
}