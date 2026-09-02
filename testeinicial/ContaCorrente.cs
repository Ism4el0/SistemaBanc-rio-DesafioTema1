namespace SistemaBancario;

public class ContaCorrente : ContaBancaria
{
    public decimal TaxaSaque { get; set; }

    public ContaCorrente(int numeroConta, string titular, decimal saldoInicial, decimal taxaSaque = 5.00m)
        : base(numeroConta, titular, saldoInicial)
    {
        TaxaSaque = taxaSaque;
    }

    public override bool Sacar(decimal valor)
    {
        decimal totalDebito = valor + TaxaSaque;

        if (valor <= 0)
        {
            Console.WriteLine("Valor de saque inválido!");
            return false;
        }

        if (Saldo < totalDebito)
        {
            Console.WriteLine($"[Conta Corrente {NumeroConta}] Saldo insuficiente! (Saque: R$ {valor:F2} + Taxa: R$ {TaxaSaque:F2} = R$ {totalDebito:F2} | Saldo: R$ {Saldo:F2})");
            return false;
        }

        Saldo -= totalDebito;
        Console.WriteLine($"[Conta Corrente {NumeroConta}] Saque de R$ {valor:F2} realizado com sucesso! (Taxa: R$ {TaxaSaque:F2})");
        return true;
    }
}
