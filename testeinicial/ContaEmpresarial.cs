namespace SistemaBancario;
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
                $"Saldo insuficiente na Conta Empresarial {NumeroConta}! Tentativa de saque: R$ {valor:N2}. Saldo disponível: R$ {Saldo:N2}.");
        }

        Saldo -= valor;
        RegistrarTransacao(TipoTransacao.Saque, valor, Saldo, "Saque empresarial");
        return true;
    }
    public bool RealizarEmprestimo(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do empréstimo deve ser maior que zero.");

        if (valor > LimiteEmprestimo)
        {
            throw new OperacaoBancariaException(
                $"Empréstimo recusado para a Conta {NumeroConta}! Limite disponível: R$ {LimiteEmprestimo:N2}. Valor solicitado: R$ {valor:N2}.");
        }

        Saldo += valor;
        LimiteEmprestimo -= valor;
        RegistrarTransacao(TipoTransacao.Emprestimo, valor, Saldo, $"Empréstimo contratado (Limite restante: R$ {LimiteEmprestimo:N2})");
        return true;
    }

    protected override void ExibirInformacoesEspecificas()
    {
        Console.WriteLine($"Limite de Empréstimo Restante: R$ {LimiteEmprestimo:N2}");
    }
}
