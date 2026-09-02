namespace SistemaBancario;

public abstract class ContaBancaria
{
    public int NumeroConta { get; set; }
    public string Titular { get; set; }
    public decimal Saldo { get; protected set; }

    public ContaBancaria(int numeroConta, string titular, decimal saldoInicial)
    {
        NumeroConta = numeroConta;
        Titular = titular;
        Saldo = saldoInicial;
    }

    public virtual void Depositar(decimal valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("O valor de depósito deve ser positivo!");
            return;
        }

        Saldo += valor;
        Console.WriteLine($"Depósito de R$ {valor:F2} realizado com sucesso na conta {NumeroConta}.");
    }

    public abstract bool Sacar(decimal valor);

    public virtual void ExibirExtrato()
    {
        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"Conta: {NumeroConta} | Titular: {Titular}");
        Console.WriteLine($"Tipo: {GetType().Name}");
        Console.WriteLine($"Saldo Atual: R$ {Saldo:F2}");
        Console.WriteLine("------------------------------------------");
    }
}
