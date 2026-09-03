namespace SistemaBancario;

/// <summary>
/// Interface que define o contrato para entidades que possuem cálculo de tributo/taxa tributária.
/// Atende ao requisito de Interfaces na Programação Orientada a Objetos.
/// </summary>
public interface ITributavel
{
    decimal CalcularTributo();
}
