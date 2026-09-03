namespace SistemaBancario;

/// <summary>
/// Representa uma Conta Corrente bancária.
/// Possui taxa fixa por operação de saque e implementa a interface ITributavel.
/// </summary>
public class ContaCorrente : ContaBancaria, ITributavel
{
    public decimal TaxaSaque { get; set; }

    public ContaCorrente(int numeroConta, string titular, decimal saldoInicial, decimal taxaSaque = 5.00m)
        : base(numeroConta, titular, saldoInicial)
    {
        if (taxaSaque < 0)
            throw new ArgumentException("A taxa de saque não pode ser negativa.");

        TaxaSaque = taxaSaque;
    }

    public override bool Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor de saque deve ser maior que zero.");

        decimal totalDebito = valor + TaxaSaque;

        if (Saldo < totalDebito)
        {
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente na Conta Corrente {NumeroConta}! Saque solicitado: R$ {valor:F2} + Taxa: R$ {TaxaSaque:F2} (Total: R$ {totalDebito:F2}). Saldo disponível: R$ {Saldo:F2}.");
        }

        Saldo -= totalDebito;
        return true;
    }

    /// <summary>
    /// Implementação da interface ITributavel: calcula 1% sobre o saldo como imposto sobre operações financeiras.
    /// </summary>
    public decimal CalcularTributo()
    {
        return Saldo * 0.01m;
    }

    public override void ExibirExtrato()
    {
        base.ExibirExtrato();
        Console.WriteLine($"Taxa por Saque: R$ {TaxaSaque:F2}");
        Console.WriteLine($"Tributo Estimado (1% do saldo): R$ {CalcularTributo():F2}");
        Console.WriteLine("------------------------------------------");
    }
}
