namespace Banking.Exceptions.ExceptionBase;

public class InfraestruturaException : BankingExceptions
{
    public InfraestruturaException(string message) : base(message)
    {
    }

    public InfraestruturaException(List<string> mensagem) : base(mensagem)
    {
    }
}