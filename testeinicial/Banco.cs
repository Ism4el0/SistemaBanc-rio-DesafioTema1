namespace SistemaBancario;
public class Banco
{
    public string Nome { get; }
    private readonly List<ContaBancaria> _contas;

    public Banco(string nome)
    {
        Nome = string.IsNullOrWhiteSpace(nome) ? "Banco Digital" : nome;
        _contas = new List<ContaBancaria>();
    }
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
    public ContaBancaria BuscarConta(int numeroConta)
    {
        var conta = _contas.FirstOrDefault(c => c.NumeroConta == numeroConta);
        if (conta == null)
        {
            throw new OperacaoBancariaException($"Conta de número {numeroConta} não encontrada no sistema.");
        }
        return conta;
    }
    public IReadOnlyList<ContaBancaria> ObterTodasContas()
    {
        return _contas.AsReadOnly();
    }
    public decimal ObterTotalCustodia()
    {
        return _contas.Sum(c => c.Saldo);
    }
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
