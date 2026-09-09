namespace SistemaBancario;

/// <summary>
/// Tipos possíveis de movimentação financeira no histórico da conta.
/// </summary>
public enum TipoTransacao
{
    AberturaConta,
    Deposito,
    Saque,
    TransferenciaEnviada,
    TransferenciaRecebida,
    PagamentoFatura,
    Rendimento,
    Emprestimo
}

/// <summary>
/// Representa uma transação registrada no histórico de uma conta bancária.
/// </summary>
public class Transacao
{
    public DateTime DataHora { get; }
    public TipoTransacao Tipo { get; }
    public decimal Valor { get; }
    public decimal SaldoAposOperacao { get; }
    public string Descricao { get; }

    public Transacao(TipoTransacao tipo, decimal valor, decimal saldoAposOperacao, string descricao)
    {
        DataHora = DateTime.Now;
        Tipo = tipo;
        Valor = valor;
        SaldoAposOperacao = saldoAposOperacao;
        Descricao = descricao;
    }

    /// <summary>
    /// Retorna uma descrição amigável do tipo da transação.
    /// </summary>
    public string ObterNomeAmigavel() => Tipo switch
    {
        TipoTransacao.AberturaConta => "Abertura de Conta",
        TipoTransacao.Deposito => "Depósito",
        TipoTransacao.Saque => "Saque",
        TipoTransacao.TransferenciaEnviada => "Transf. Enviada",
        TipoTransacao.TransferenciaRecebida => "Transf. Recebida",
        TipoTransacao.PagamentoFatura => "Pagamento Fatura",
        TipoTransacao.Rendimento => "Rendimento",
        TipoTransacao.Emprestimo => "Empréstimo",
        _ => Tipo.ToString()
    };
}
