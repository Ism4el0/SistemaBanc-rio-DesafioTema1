# Sistema Bancário - Desafio Tema 1 (POO em C#)

Projeto desenvolvido em C# (.NET 10) aplicando os conceitos fundamentais de **Programação Orientada a Objetos (POO)** e boas práticas de desenvolvimento de software em modo console, em conformidade com o desafio de sprint.

---

## 🎯 Conceitos de POO Aplicados

1. **Abstração**:
   - Classe abstrata `ContaBancaria` que define os atributos essenciais (`NumeroConta`, `Titular`, `Saldo`) e comportamentos comuns (`Depositar`, `Transferir`, `ExibirExtrato`), além do contrato abstrato para `Sacar`.

2. **Encapsulamento**:
   - Modificadores de acesso restritos (`private set`, `protected set`), impedindo alteração direta do saldo ou número de conta fora dos métodos de negócio.

3. **Herança**:
   - As classes especializadas `ContaCorrente`, `ContaPoupanca` e `ContaEmpresarial` reutilizam e estendem as características da classe base `ContaBancaria`.

4. **Polimorfismo**:
   - **Sobrescrita de métodos (`override`)**: O método `Sacar(valor)` possui comportamento único para cada tipo de conta (desconto de taxa na Corrente, saque livre na Poupança e verificação de saldo na Empresarial).
   - **Polimorfismo de interface e coleções**: Manipulação de diferentes tipos de conta através de referências genéricas `ContaBancaria` e `ITributavel` em laços de repetição.

5. **Interface**:
   - Interface `ITributavel` com o método `decimal CalcularTributo()`, implementada por `ContaCorrente` para demonstrar desacoplamento e contratos de capacidade.

6. **Tratamento de Exceções**:
   - Exceções personalizadas (`SaldoInsuficienteException`, `OperacaoBancariaException`) e captura de exceções do sistema (`ArgumentException`, `FormatException`, `ArgumentNullException`) com blocos `try-catch`, protegendo a aplicação contra quebras por entradas inválidas.

7. **Coleções e Estruturas de Repetição**:
   - Gerenciamento de contas dinâmico via `List<ContaBancaria>` e LINQ na classe `Banco`.
   - Menu em console interativo baseado em loop contínuo (`while`) e estruturas condicionais (`switch-case`).

---

## 🏗️ Estrutura das Classes

- **`ContaBancaria`** *(abstract)*: Classe base com dados do titular, saldo e operações de depósito e transferência.
- **`ContaCorrente`**: Aplica taxa fixa por saque (R$ 5,00) e implementa a interface `ITributavel`.
- **`ContaPoupanca`**: Isenta de taxas de saque e provê o método `AplicarRendimento(taxa)`.
- **`ContaEmpresarial`**: Possui limite de crédito para empréstimos (`RealizarEmprestimo`).
- **`ITributavel`** *(interface)*: Contrato para cálculo de tributos sobre saldos/operações.
- **`Banco`**: Gerenciador da coleção de contas, responsável por busca, total sob custódia e cálculo consolidado de tributos.
- **`SaldoInsuficienteException`**: Exceção lançada quando uma tentativa de saque ou transferência ultrapassa o saldo disponível.
- **`OperacaoBancariaException`**: Exceção de negócio para violações de limites ou regras bancárias.
- **`Program.cs`**: Menu interativo em console com opções completas para o usuário.

---

## 🖥️ Menu Interativo do Sistema

O sistema disponibiliza as seguintes opções no terminal:
1. **Abrir Nova Conta** (Corrente, Poupança ou Empresarial)
2. **Consultar Extrato de uma Conta**
3. **Realizar Depósito**
4. **Realizar Saque** (com validação de regras e taxas por tipo de conta)
5. **Realizar Transferência entre Contas**
6. **Aplicar Rendimento** (exclusivo Conta Poupança)
7. **Solicitar Empréstimo** (exclusivo Conta Empresarial)
8. **Listar Todas as Contas** (demonstração de polimorfismo)
9. **Relatório de Tributos** (demonstração da interface `ITributavel`)
0. **Sair**

---

## 🚀 Como Executar

No terminal, acesse a pasta do projeto e execute:

```bash
cd testeinicial
dotnet run
```
