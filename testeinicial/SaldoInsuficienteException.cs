namespace SistemaBancario;
public class SaldoInsuficienteException : Exception
{
    public SaldoInsuficienteException(string message) : base(message)
    {
    }

    public SaldoInsuficienteException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
