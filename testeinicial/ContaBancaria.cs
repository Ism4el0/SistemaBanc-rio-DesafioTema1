namespace SistemaBancario;
public abstract class ContaBancaria
{
    public int NumeroConta { get; private set; }
    public string Titular { get; set; }
    public decimal Saldo { get; protected set; }
    private readonly List<Transacao> _historico = new();

    public ContaBancaria(int numeroConta, string titular, decimal saldoInicial)
    {
        if (numeroConta <= 0)
            throw new ArgumentException("O número da conta deve ser um valor positivo.");

        if (string.IsNullOrWhiteSpace(titular))
            throw new ArgumentException("O nome do titular é obrigatório.");

        if (saldoInicial < 0)
            throw new ArgumentException("O saldo inicial não pode ser negativo.");

        NumeroConta = numeroConta;
        Titular = titular;
        Saldo = saldoInicial;

        if (saldoInicial > 0)
        {
            RegistrarTransacao(TipoTransacao.AberturaConta, saldoInicial, Saldo, "Depósito de abertura da conta");
        }
    }
    public IReadOnlyList<Transacao> ObterHistorico() => _historico.AsReadOnly();

    protected void RegistrarTransacao(TipoTransacao tipo, decimal valor, decimal saldoApos, string descricao)
    {
        _historico.Add(new Transacao(tipo, valor, saldoApos, descricao));
    }
    public virtual void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor de depósito deve ser maior que zero.");

        Saldo += valor;
        RegistrarTransacao(TipoTransacao.Deposito, valor, Saldo, "Depósito em conta");
    }
    public abstract bool Sacar(decimal valor);
    public virtual void Transferir(decimal valor, ContaBancaria contaDestino)
    {
        if (contaDestino == null)
            throw new ArgumentNullException(nameof(contaDestino), "Conta de destino não informada.");

        if (contaDestino.NumeroConta == this.NumeroConta)
            throw new OperacaoBancariaException("A conta de destino não pode ser a mesma conta de origem.");

        if (valor <= 0)
            throw new ArgumentException("O valor de transferência deve ser maior que zero.");

        if (Saldo < valor)
        {
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente na Conta {NumeroConta}! Tentativa de transferência: R$ {valor:N2}. Saldo disponível: R$ {Saldo:N2}.");
        }

        Saldo -= valor;
        RegistrarTransacao(TipoTransacao.TransferenciaEnviada, valor, Saldo, $"Transferido para {contaDestino.Titular} (Conta {contaDestino.NumeroConta})");
        contaDestino.ReceberTransferencia(valor, this);
    }
    public virtual void ReceberTransferencia(decimal valor, ContaBancaria contaOrigem)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor recebido deve ser maior que zero.");

        Saldo += valor;
        RegistrarTransacao(TipoTransacao.TransferenciaRecebida, valor, Saldo, $"Recebido de {contaOrigem.Titular} (Conta {contaOrigem.NumeroConta})");
    }
    public virtual void PagarConta(decimal valor, string descricao)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do pagamento deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição ou código da fatura deve ser informado.");

        if (Saldo < valor)
        {
            throw new SaldoInsuficienteException(
                $"Saldo insuficiente para pagar a fatura '{descricao}'! Valor: R$ {valor:N2} | Saldo disponível: R$ {Saldo:N2}.");
        }

        Saldo -= valor;
        RegistrarTransacao(TipoTransacao.PagamentoFatura, valor, Saldo, $"Pagamento: {descricao}");
    }
    protected virtual void ExibirInformacoesEspecificas()
    {
    }
    public virtual void ExibirExtrato(bool incluirHistorico = true)
    {
        Console.WriteLine("--------------------------------------------------------------------------------");
        Console.WriteLine($"Conta: {NumeroConta} | Titular: {Titular}");
        Console.WriteLine($"Tipo: {GetType().Name} | Saldo Atual: R$ {Saldo:N2}");
        ExibirInformacoesEspecificas();

        if (incluirHistorico)
        {
            ExibirHistoricoTransacoes();
        }
        Console.WriteLine("--------------------------------------------------------------------------------");
    }
    public void ExibirHistoricoTransacoes()
    {
        Console.WriteLine("\n--- HISTÓRICO DE TRANSAÇÕES ---");
        if (_historico.Count == 0)
        {
            Console.WriteLine("Nenhuma transação registrada até o momento.");
            return;
        }

        Console.WriteLine($"{"Data/Hora",-20} | {"Tipo",-20} | {"Valor",-16} | {"Saldo Após",-16} | Descrição");
        Console.WriteLine(new string('-', 95));

        foreach (var t in _historico)
        {
            string sinal = t.Tipo switch
            {
                TipoTransacao.Deposito or
                TipoTransacao.TransferenciaRecebida or
                TipoTransacao.Rendimento or
                TipoTransacao.Emprestimo or
                TipoTransacao.AberturaConta => "+",
                _ => "-"
            };

            string valorFormatado = $"{sinal} R$ {t.Valor:N2}";
            string saldoFormatado = $"R$ {t.SaldoAposOperacao:N2}";

            Console.WriteLine($"{t.DataHora:dd/MM/yyyy HH:mm:ss} | {t.ObterNomeAmigavel(),-20} | {valorFormatado,-16} | {saldoFormatado,-16} | {t.Descricao}");
        }
    }
}
