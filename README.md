# Sistema Bancário - Desafio Tema 1

Projeto desenvolvido aplicando conceitos fundamentais de Programação Orientada a Objetos (POO): Herança, Polimorfismo, Abstração e Encapsulamento.

Estrutura do Projeto

- **`ContaBancaria`** (Classe Abstrata Pai): Base para todas as contas, contendo `NumeroConta`, `Titular`, `Saldo`, método de depósito e método abstrato de saque.
- **`ContaCorrente`**: Aplica uma taxa fixa a cada saque realizado.
- **`ContaPoupanca`**: Isenta de taxas de saque e possui funcionalidade de rendimento (`AplicarRendimento`).
- **`ContaEmpresarial`**: Possui funcionalidade exclusiva de limite de empréstimo (`RealizarEmprestimo`).
- **`Program.cs`**: Execução de testes demonstrando as operações e polimorfismo via `List<ContaBancaria>`.
