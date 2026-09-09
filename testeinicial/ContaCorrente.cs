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
                $"Saldo insuficiente na Conta Corrente {NumeroConta}! Saque solicitado: R$ {valor:N2} + Taxa: R$ {TaxaSaque:N2} (Total: R$ {totalDebito:N2}). Saldo disponível: R$ {Saldo:N2}.");
        }

        Saldo -= totalDebito;
        RegistrarTransacao(TipoTransacao.Saque, valor, Saldo, $"Saque em dinheiro (Taxa aplicada: R$ {TaxaSaque:N2})");
        return true;
    }

    public override void Transferir(decimal valor, ContaBancaria contaDestino)
    {
        if (contaDestino == null)
            throw new ArgumentNullException(nameof(contaDestino), "Conta de destino não informada.");

        if (contaDestino.NumeroConta == this.NumeroConta)
            throw new OperacaoBancariaException("A conta de destino não pode ser a mesma conta de origem.");

        if (valor <= 0)
            throw new ArgumentException("O valor de transferência deve ser maior que zero.");

        decimal totalDebito = valor + TaxaSaque;

        if (Saldo < totalDebito)
        {
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente na Conta Corrente {NumeroConta}! Transferência solicitada: R$ {valor:N2} + Taxa: R$ {TaxaSaque:N2} (Total: R$ {totalDebito:N2}). Saldo disponível: R$ {Saldo:N2}.");
        }

        Saldo -= totalDebito;
        RegistrarTransacao(TipoTransacao.TransferenciaEnviada, valor, Saldo, $"Transf. para {contaDestino.Titular} (Conta {contaDestino.NumeroConta}) - Taxa: R$ {TaxaSaque:N2}");
        contaDestino.ReceberTransferencia(valor, this);
    }

    /// <summary>
    /// Implementação da interface ITributavel: calcula 1% sobre o saldo como imposto sobre operações financeiras.
    /// </summary>
    public decimal CalcularTributo()
    {
        return Saldo * 0.01m;
    }

    protected override void ExibirInformacoesEspecificas()
    {
        Console.WriteLine($"Taxa por Operação: R$ {TaxaSaque:N2}");
        Console.WriteLine($"Tributo Estimado (1% do saldo): R$ {CalcularTributo():N2}");
    }
}
