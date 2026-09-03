namespace SistemaBancario;

/// <summary>
/// Representa uma Conta Empresarial (Pessoa Jurídica).
/// Possui um limite específico para contratação de empréstimos.
/// </summary>
public class ContaEmpresarial : ContaBancaria
{
    public decimal LimiteEmprestimo { get; private set; }

    public ContaEmpresarial(int numeroConta, string titular, decimal saldoInicial, decimal limiteEmprestimo)
        : base(numeroConta, titular, saldoInicial)
    {
        if (limiteEmprestimo < 0)
            throw new ArgumentException("O limite de empréstimo não pode ser negativo.");

        LimiteEmprestimo = limiteEmprestimo;
    }

    public override bool Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor de saque deve ser maior que zero.");

        if (Saldo < valor)
        {
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente na Conta Empresarial {NumeroConta}! Tentativa de saque: R$ {valor:F2}. Saldo disponível: R$ {Saldo:F2}.");
        }

        Saldo -= valor;
        return true;
    }

    /// <summary>
    /// Contrata um empréstimo creditando o saldo e debitando o limite disponível.
    /// </summary>
    public bool RealizarEmprestimo(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do empréstimo deve ser maior que zero.");

        if (valor > LimiteEmprestimo)
        {
            throw new OperacaoBancariaException(
                $"Empréstimo recusado para a Conta {NumeroConta}! Limite disponível: R$ {LimiteEmprestimo:F2}. Valor solicitado: R$ {valor:F2}.");
        }

        Saldo += valor;
        LimiteEmprestimo -= valor;
        return true;
    }

    public override void ExibirExtrato()
    {
        base.ExibirExtrato();
        Console.WriteLine($"Limite de Empréstimo Restante: R$ {LimiteEmprestimo:F2}");
        Console.WriteLine("------------------------------------------");
    }
}
