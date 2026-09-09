namespace SistemaBancario;

/// <summary>
/// Classe abstrata base que representa uma conta bancária genérica.
/// Aplica os conceitos de Abstração, Encapsulamento e base para Herança e Polimorfismo.
/// </summary>
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

    /// <summary>
    /// Retorna a lista de transações registradas na conta (somente leitura).
    /// </summary>
    public IReadOnlyList<Transacao> ObterHistorico() => _historico.AsReadOnly();

    /// <summary>
    /// Registra uma nova movimentação financeira no histórico da conta.
    /// </summary>
    protected void RegistrarTransacao(TipoTransacao tipo, decimal valor, decimal saldoApos, string descricao)
    {
        _historico.Add(new Transacao(tipo, valor, saldoApos, descricao));
    }

    /// <summary>
    /// Realiza o depósito de um valor na conta.
    /// </summary>
    public virtual void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor de depósito deve ser maior que zero.");

        Saldo += valor;
        RegistrarTransacao(TipoTransacao.Deposito, valor, Saldo, "Depósito em conta");
    }

    /// <summary>
    /// Método abstrato de saque que deve ser implementado pelas classes derivadas (Polimorfismo).
    /// </summary>
    public abstract bool Sacar(decimal valor);

    /// <summary>
    /// Realiza a transferência de um valor para outra conta bancária.
    /// </summary>
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

    /// <summary>
    /// Credita valor transferido de outra conta bancária.
    /// </summary>
    public virtual void ReceberTransferencia(decimal valor, ContaBancaria contaOrigem)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor recebido deve ser maior que zero.");

        Saldo += valor;
        RegistrarTransacao(TipoTransacao.TransferenciaRecebida, valor, Saldo, $"Recebido de {contaOrigem.Titular} (Conta {contaOrigem.NumeroConta})");
    }

    /// <summary>
    /// Realiza o pagamento de uma conta/fatura debitando diretamente do saldo da conta bancária.
    /// </summary>
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

    /// <summary>
    /// Hook virtual para classes filhas exibirem dados específicos (ex: taxas, limites).
    /// </summary>
    protected virtual void ExibirInformacoesEspecificas()
    {
    }

    /// <summary>
    /// Exibe os dados, o saldo da conta e opcionalmente o histórico completo de transações.
    /// </summary>
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

    /// <summary>
    /// Exibe no console a listagem formatada de todas as transações realizadas na conta.
    /// </summary>
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
