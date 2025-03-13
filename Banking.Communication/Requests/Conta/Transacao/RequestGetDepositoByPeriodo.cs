namespace Banking.Communication.Requests.Conta.Transacao;

public class RequestGetDepositoByPeriodo
{
    public required string DataInicial { get; set; }
    public required string DataFinal { get; set; }
}