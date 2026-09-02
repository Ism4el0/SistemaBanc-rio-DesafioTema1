namespace SistemaBancario;

public class ContaPoupanca : ContaBancaria
{
    public ContaPoupanca(int numeroConta, string titular, decimal saldoInicial)
        : base(numeroConta, titular, saldoInicial)
    {
    }

    public override bool Sacar(decimal valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor de saque inválido!");
            return false;
        }

        if (Saldo < valor)
        {
            Console.WriteLine($"[Conta Poupança {NumeroConta}] Saldo insuficiente! (Tentativa: R$ {valor:F2} | Saldo: R$ {Saldo:F2})");
            return false;
        }

        Saldo -= valor;
        Console.WriteLine($"[Conta Poupança {NumeroConta}] Saque de R$ {valor:F2} realizado com sucesso sem taxas!");
        return true;
    }

    public void AplicarRendimento(decimal taxaPercentual)
    {
        if (taxaPercentual <= 0)
        {
            Console.WriteLine("A taxa de rendimento deve ser maior que zero!");
            return;
        }

        decimal rendimento = Saldo * (taxaPercentual / 100);
        Saldo += rendimento;
        Console.WriteLine($"[Conta Poupança {NumeroConta}] Rendimento de {taxaPercentual}% aplicado (+ R$ {rendimento:F2}). Novo Saldo: R$ {Saldo:F2}");
    }
}
