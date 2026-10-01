namespace SistemaBancario;
public class OperacaoBancariaException : Exception
{
    public OperacaoBancariaException(string message) : base(message)
    {
    }

    public OperacaoBancariaException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
