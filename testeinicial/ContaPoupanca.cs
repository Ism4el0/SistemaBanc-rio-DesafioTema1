namespace SistemaBancario;

/// <summary>
/// Representa uma Conta Poupança bancária.
/// É isenta de taxas para saque e possui a operação de aplicar rendimento sobre o saldo.
/// </summary>
public class ContaPoupanca : ContaBancaria
{
    public ContaPoupanca(int numeroConta, string titular, decimal saldoInicial)
        : base(numeroConta, titular, saldoInicial)
    {
    }

    public override bool Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor de saque deve ser maior que zero.");

        if (Saldo < valor)
        {
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente na Conta Poupança {NumeroConta}! Tentativa de saque: R$ {valor:F2}. Saldo disponível: R$ {Saldo:F2}.");
        }

        Saldo -= valor;
        return true;
    }

    /// <summary>
    /// Aplica uma taxa percentual de rendimento sobre o saldo atual.
    /// </summary>
    public decimal AplicarRendimento(decimal taxaPercentual)
    {
        if (taxaPercentual <= 0)
            throw new ArgumentException("A taxa de rendimento deve ser maior que zero.");

        decimal rendimento = Saldo * (taxaPercentual / 100m);
        Saldo += rendimento;
        return rendimento;
    }

    public override void ExibirExtrato()
    {
        base.ExibirExtrato();
        Console.WriteLine("Benefício: Isenta de tarifas de saque");
        Console.WriteLine("------------------------------------------");
    }
}
