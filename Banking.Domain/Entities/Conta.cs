namespace Banking.Domain.Entities;

public class Conta
{
    public int Id { get; set; }
    public int NumeroAgencia { get; set; }
    public int NumeroBanco { get; set; }
    public int NumeroConta { get; set; }
    public double Saldo
    {
        get
        {
            return _saldo;
        }
        set
        {
            _saldo = value;
        }
    } 

    private double _saldo;

    public Conta(int numeroAgencia, int numeroBanco, int numeroConta, Guid userIdentifier)
    {
        NumeroAgencia = numeroAgencia;
        NumeroBanco = numeroBanco;
        NumeroConta = numeroConta;
        _saldo = 0;
        UserIdentifier = userIdentifier;
        DataCriacao = DateTime.UtcNow;
    }

    public Guid UserIdentifier { get; set; }
    public DateTime DataCriacao { get; set; }


    public void AdicionarSaldo(double valor)
    {
        if (valor < 0)
            throw new InvalidOperationException("O valor a ser adicionado não pode ser negativo.");

        _saldo += valor;
    }

    public void RemoverSaldo(double valor)
    {
        if (valor <= 0)
            throw new InvalidOperationException("O valor a ser removido deve ser positivo.");

        if (valor > _saldo)
            throw new InvalidOperationException("Saldo insuficiente.");

        _saldo -= valor;
    }
}
