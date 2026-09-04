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

- **`ContaBancaria`** *(abstract)*: Classe base com dados do titular, saldo e operações de depósito, transferência e pagamento de contas/faturas.
- **`ContaCorrente`**: Aplica taxa fixa por saque (R$ 5,00) e implementa a interface `ITributavel`.
- **`ContaPoupanca`**: Isenta de taxas de saque e provê o método `AplicarRendimento(taxa)`.
- **`ContaEmpresarial`**: Possui limite de crédito para empréstimos (`RealizarEmprestimo`).
- **`ITributavel`** *(interface)*: Contrato para cálculo de tributos sobre saldos/operações.
- **`Banco`**: Gerenciador da coleção de contas, responsável por busca, total sob custódia e cálculo consolidado de tributos.
- **`SaldoInsuficienteException`**: Exceção lançada quando uma tentativa de saque ou transferência ultrapassa o saldo disponível.
- **`OperacaoBancariaException`**: Exceção de negócio para violações de limites ou regras bancárias.
- **`Program.cs`**: Menu interativo em console com opções completas para o usuário.

---

## 🖥️ Menus do Sistema

### Menu Principal
1. **Acessar Minha Conta (Login)** (Acesso à sessão da conta informando apenas o número)
2. **Abrir Nova Conta** (Corrente, Poupança ou Empresarial)
3. **Listar Todas as Contas** (Painel geral com demonstração de polimorfismo)
4. **Relatório de Tributos** (Demonstração da interface `ITributavel`)
0. **Sair**

### Menu da Conta (Sessão do Usuário Logado)
1. **Consultar Extrato** (Exibição detalhada de saldo e informações da conta)
2. **Realizar Depósito**
3. **Realizar Saque** (com validação de regras e taxas por tipo de conta)
4. **Realizar Transferência entre Contas**
5. **Pagar Conta / Fatura** (Débito direto do saldo com validações de negócio)
6. **Operação Especial** (`Aplicar Rendimento` para Poupança / `Solicitar Empréstimo` para Empresarial)
0. **Sair da Conta (Logout)**

---

## 🚀 Como Executar

No terminal, acesse a pasta do projeto e execute:

```bash
cd testeinicial
dotnet run
```
