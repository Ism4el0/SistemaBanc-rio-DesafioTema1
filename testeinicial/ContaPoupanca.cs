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
            throw new ArgumentException("O valor de saque deve ser maior que zero.");

        if (Saldo < valor)
        {
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente na Conta Poupança {NumeroConta}! Tentativa de saque: R$ {valor:N2}. Saldo disponível: R$ {Saldo:N2}.");
        }

        Saldo -= valor;
        RegistrarTransacao(TipoTransacao.Saque, valor, Saldo, "Saque em dinheiro");
        return true;
    }
    public decimal AplicarRendimento(decimal taxaPercentual)
    {
        if (taxaPercentual <= 0)
            throw new ArgumentException("A taxa de rendimento deve ser maior que zero.");

        decimal rendimento = Saldo * (taxaPercentual / 100m);
        Saldo += rendimento;
        RegistrarTransacao(TipoTransacao.Rendimento, rendimento, Saldo, $"Rendimento de {taxaPercentual:N2}% aplicado");
        return rendimento;
    }

    protected override void ExibirInformacoesEspecificas()
    {
        Console.WriteLine("Benefício: Isenta de tarifas de saque");
    }
}
