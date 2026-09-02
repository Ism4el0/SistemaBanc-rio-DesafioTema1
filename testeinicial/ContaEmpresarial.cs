namespace SistemaBancario;

public class ContaEmpresarial : ContaBancaria
{
    public decimal LimiteEmprestimo { get; private set; }

    public ContaEmpresarial(int numeroConta, string titular, decimal saldoInicial, decimal limiteEmprestimo)
        : base(numeroConta, titular, saldoInicial)
    {
        LimiteEmprestimo = limiteEmprestimo;
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
            Console.WriteLine($"[Conta Empresarial {NumeroConta}] Saldo insuficiente para saque!");
            return false;
        }

        Saldo -= valor;
        Console.WriteLine($"[Conta Empresarial {NumeroConta}] Saque de R$ {valor:F2} realizado.");
        return true;
    }

    public bool RealizarEmprestimo(decimal valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor do empréstimo inválido!");
            return false;
        }

        if (valor > LimiteEmprestimo)
        {
            Console.WriteLine($"[Conta Empresarial {NumeroConta}] Empréstimo recusado! Limite disponível: R$ {LimiteEmprestimo:F2}");
            return false;
        }

        Saldo += valor;
        LimiteEmprestimo -= valor;
        Console.WriteLine($"[Conta Empresarial {NumeroConta}] Empréstimo de R$ {valor:F2} aprovado! Limite restante: R$ {LimiteEmprestimo:F2}");
        return true;
    }

    public override void ExibirExtrato()
    {
        base.ExibirExtrato();
        Console.WriteLine($"Limite de Empréstimo Restante: R$ {LimiteEmprestimo:F2}");
        Console.WriteLine("------------------------------------------");
    }
}
