namespace SistemaBancario;

/// <summary>
/// Exceção lançada quando uma operação de saque ou transferência não possui saldo suficiente.
/// </summary>
public class SaldoInsuficienteException : Exception
{
    public SaldoInsuficienteException(string message) : base(message)
    {
    }

    public SaldoInsuficienteException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
