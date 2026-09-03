namespace SistemaBancario;

/// <summary>
/// Classe que gerencia a coleção de contas bancárias e operações agregadas.
/// Aplica conceitos de Coleções (List), Encapsulamento e Polimorfismo com Interfaces.
/// </summary>
public class Banco
{
    public string Nome { get; }
    private readonly List<ContaBancaria> _contas;

    public Banco(string nome)
    {
        Nome = string.IsNullOrWhiteSpace(nome) ? "Banco Digital" : nome;
        _contas = new List<ContaBancaria>();
    }

    /// <summary>
    /// Adiciona uma nova conta ao banco, garantindo unicidade do número da conta.
    /// </summary>
    public void AdicionarConta(ContaBancaria conta)
    {
        if (conta == null)
            throw new ArgumentNullException(nameof(conta), "A conta não pode ser nula.");

        if (_contas.Any(c => c.NumeroConta == conta.NumeroConta))
        {
            throw new OperacaoBancariaException($"Já existe uma conta cadastrada com o número {conta.NumeroConta}.");
        }

        _contas.Add(conta);
    }

    /// <summary>
    /// Busca uma conta pelo seu número identificador.
    /// </summary>
    public ContaBancaria BuscarConta(int numeroConta)
    {
        var conta = _contas.FirstOrDefault(c => c.NumeroConta == numeroConta);
        if (conta == null)
        {
            throw new OperacaoBancariaException($"Conta de número {numeroConta} não encontrada no sistema.");
        }
        return conta;
    }

    /// <summary>
    /// Retorna todas as contas registradas no banco.
    /// </summary>
    public IReadOnlyList<ContaBancaria> ObterTodasContas()
    {
        return _contas.AsReadOnly();
    }

    /// <summary>
    /// Calcula a soma total de saldo sob custódia do banco.
    /// </summary>
    public decimal ObterTotalCustodia()
    {
        return _contas.Sum(c => c.Saldo);
    }

    /// <summary>
    /// Itera sobre todas as contas que implementam a interface ITributavel (Polimorfismo de Interface).
    /// </summary>
    public decimal CalcularTotalTributos()
    {
        decimal total = 0m;
        foreach (var tributavel in _contas.OfType<ITributavel>())
        {
            total += tributavel.CalcularTributo();
        }
        return total;
    }
}
