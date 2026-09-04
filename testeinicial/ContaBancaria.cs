namespace SistemaBancario;

/// <summary>
/// Classe abstrata base que representa uma conta bancária genérica.
/// Aplica os conceitos de Abstração, Encapsulamento e base para Herança e Polimorfismo.
/// </summary>
public abstract class ContaBancaria
{
    public int NumeroConta { get; private set; }
    public string Titular { get; set; }
    public decimal Saldo { get; protected set; }

    public ContaBancaria(int numeroConta, string titular, decimal saldoInicial)
    {
        if (numeroConta <= 0)
            throw new ArgumentException("O número da conta deve ser um valor positivo.");

        if (string.IsNullOrWhiteSpace(titular))
            throw new ArgumentException("O nome do titular é obrigatório.");

        if (saldoInicial < 0)
            throw new ArgumentException("O saldo inicial não pode ser negativo.");

        NumeroConta = numeroConta;
        Titular = titular;
        Saldo = saldoInicial;
    }

    /// <summary>
    /// Realiza o depósito de um valor na conta.
    /// </summary>
    public virtual void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor de depósito deve ser maior que zero.");

        Saldo += valor;
    }

    /// <summary>
    /// Método abstrato de saque que deve ser implementado pelas classes derivadas (Polimorfismo).
    /// </summary>
    public abstract bool Sacar(decimal valor);

    /// <summary>
    /// Realiza a transferência de um valor para outra conta bancária.
    /// </summary>
    public virtual void Transferir(decimal valor, ContaBancaria contaDestino)
    {
        if (contaDestino == null)
            throw new ArgumentNullException(nameof(contaDestino), "Conta de destino não informada.");

        if (contaDestino.NumeroConta == this.NumeroConta)
            throw new OperacaoBancariaException("A conta de destino não pode ser a mesma conta de origem.");

        Sacar(valor);
        contaDestino.Depositar(valor);
    }

    /// <summary>
    /// Realiza o pagamento de uma conta/fatura debitando diretamente do saldo da conta bancária.
    /// </summary>
    public virtual void PagarConta(decimal valor, string descricao)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do pagamento deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição ou código da fatura deve ser informado.");

        if (Saldo < valor)
        {
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente para pagar a fatura '{descricao}'! Valor: R$ {valor:F2} | Saldo disponível: R$ {Saldo:F2}.");
        }

        Saldo -= valor;
    }

    /// <summary>
    /// Exibe os dados e o saldo da conta no console.
    /// </summary>
    public virtual void ExibirExtrato()
    {
        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"Conta: {NumeroConta} | Titular: {Titular}");
        Console.WriteLine($"Tipo: {GetType().Name}");
        Console.WriteLine($"Saldo Atual: R$ {Saldo:F2}");
        Console.WriteLine("------------------------------------------");
    }
}
