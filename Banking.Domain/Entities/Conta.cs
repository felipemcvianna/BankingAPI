namespace Banking.Domain.Entities;

public class Conta
{
    public int Id { get; set; }
    public int NumeroAgencia { get; set; }
    public int NumeroBanco { get; set; }
    public int NumeroConta { get; set; }

    public double Saldo
    {
        get => _saldo;
        private set => _saldo = value;
    }

    private double _saldo;
    public Guid UserIdentifier { get; set; }
    public DateTime DataCriacao { get; private set; }

    public IList<Saque> Saques { get; private set; }
    public IList<Deposito> Depositos { get; private set; }
    public IList<Transferencia> Transferencias { get; private set; }

    public Conta(int numeroAgencia, int numeroBanco, int numeroConta, Guid userIdentifier)
    {
        NumeroAgencia = numeroAgencia;
        NumeroBanco = numeroBanco;
        NumeroConta = numeroConta;
        UserIdentifier = userIdentifier;
        DataCriacao = DateTime.UtcNow;
        Saques = new List<Saque>();
        Depositos = new List<Deposito>();
        Transferencias = new List<Transferencia>();
    }


    public void AdicionarSaldo(double valor)
    {
        if (valor < 0)
            throw new InvalidOperationException("O valor a ser adicionado n�o pode ser negativo.");

        Saldo += valor;
    }

    public void RemoverSaldo(double valor)
    {
        if (valor <= 0)
            throw new InvalidOperationException("O valor a ser removido deve ser positivo.");

        if (valor > _saldo)
            throw new InvalidOperationException("Saldo insuficiente.");

        Saldo -= valor;
    }

    public void AdicionarDeposito(Deposito deposito)
    {
        Depositos.Add(deposito);
    }

    public void AdicionarSaques(Saque saque)
    {
        Saques.Add(saque);
    }

    public void AdicionarTransferencia(Transferencia transferencia)
    {
        Transferencias.Add(transferencia);
    }
}