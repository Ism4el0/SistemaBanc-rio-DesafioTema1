namespace SistemaBancario;

/// <summary>
/// Exceção de negócio lançada para falhas em regras de operações bancárias (ex: limites excedidos, contas não encontradas).
/// </summary>
public class OperacaoBancariaException : Exception
{
    public OperacaoBancariaException(string message) : base(message)
    {
    }

    public OperacaoBancariaException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
