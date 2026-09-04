using System;
using System.Globalization;
using SistemaBancario;

// Define a cultura padrão para formatação e leitura numérica
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

Banco banco = new Banco("NuNu Banquinho");

// Inicialização com dados de demonstração
InicializarDadosDemonstracao(banco);

bool executando = true;

while (executando)
{
    LimparTela();
    Console.WriteLine("=================================================");
    Console.WriteLine($"       SISTEMA BANCÁRIO - {banco.Nome.ToUpper()}");
    Console.WriteLine("=================================================");
    Console.WriteLine(" 1. Acessar Minha Conta");
    Console.WriteLine(" 2. Abrir Nova Conta");
    Console.WriteLine(" 3. Listar Todas as Contas");
    Console.WriteLine(" 4. Relatório de Tributos");
    Console.WriteLine(" 0. Sair");
    Console.WriteLine("=================================================");
    Console.Write("Escolha uma opção: ");

    string? opcao = Console.ReadLine();
    if (opcao == null)
    {
        break;
    }
    Console.WriteLine();

    try
    {
        switch (opcao?.Trim())
        {
            case "1":
                AcessarConta(banco);
                break;
            case "2":
                CadastrarNovaConta(banco);
                break;
            case "3":
                ListarContas(banco);
                break;
            case "4":
                ExibirRelatorioTributos(banco);
                break;
            case "0":
                executando = false;
                Console.WriteLine("Obrigado por utilizar nosso Sistema Bancário! Até logo.");
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Opção inválida! Por favor, escolha uma opção entre 0 e 4.");
                Console.ResetColor();
                break;
        }
    }
    catch (SaldoInsuficienteException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n[ERRO DE SALDO] {ex.Message}");
        Console.ResetColor();
    }
    catch (OperacaoBancariaException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n[REGRA DE NEGÓCIO] {ex.Message}");
        Console.ResetColor();
    }
    catch (ArgumentException ex)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n[DADO INVÁLIDO] {ex.Message}");
        Console.ResetColor();
    }
    catch (FormatException)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n[FORMATO INVÁLIDO] O valor digitado precisa ser numérico.");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"\n[ERRO INESPERADO] {ex.Message}");
        Console.ResetColor();
    }

    if (executando)
    {
        AguardarTecla();
    }
}

static void InicializarDadosDemonstracao(Banco banco)
{
    banco.AdicionarConta(new ContaCorrente(101, "João Silva", 1500.00m, 5.00m));
    banco.AdicionarConta(new ContaPoupanca(202, "Maria Oliveira", 3000.00m));
    banco.AdicionarConta(new ContaEmpresarial(303, "Tech Solutions Ltda", 10000.00m, 25000.00m));
}

static void AcessarConta(Banco banco)
{
    Console.WriteLine("--- ACESSO À CONTA ---");
    int numero = LerInteiro("Digite o número da sua conta: ");
    ContaBancaria conta = banco.BuscarConta(numero);

    MenuContaLogada(banco, conta);
}

static void MenuContaLogada(Banco banco, ContaBancaria conta)
{
    bool logado = true;
    while (logado)
    {
        LimparTela();
        Console.WriteLine("=================================================");
        Console.WriteLine($"  CONTA: {conta.NumeroConta} | TITULAR: {conta.Titular.ToUpper()}");
        Console.WriteLine($"  TIPO: {conta.GetType().Name} | SALDO: R$ {conta.Saldo:F2}");
        Console.WriteLine("=================================================");
        Console.WriteLine(" 1. Consultar Extrato");
        Console.WriteLine(" 2. Realizar Depósito");
        Console.WriteLine(" 3. Realizar Saque");
        Console.WriteLine(" 4. Realizar Transferência");
        Console.WriteLine(" 5. Pagar Conta / Fatura");
        if (conta is ContaPoupanca)
            Console.WriteLine(" 6. Aplicar Rendimento");
        else if (conta is ContaEmpresarial)
            Console.WriteLine(" 6. Solicitar Empréstimo");
        Console.WriteLine(" 0. Sair da Conta");
        Console.WriteLine("=================================================");
        Console.Write("Escolha uma opção: ");

        string? op = Console.ReadLine();
        if (op == null)
        {
            break;
        }
        Console.WriteLine();

        try
        {
            switch (op?.Trim())
            {
                case "1":
                    conta.ExibirExtrato();
                    break;
                case "2":
                    RealizarDeposito(conta);
                    break;
                case "3":
                    RealizarSaque(conta);
                    break;
                case "4":
                    RealizarTransferencia(banco, conta);
                    break;
                case "5":
                    PagarFatura(conta);
                    break;
                case "6":
                    if (conta is ContaPoupanca cp)
                        AplicarRendimento(cp);
                    else if (conta is ContaEmpresarial ce)
                        SolicitarEmprestimo(ce);
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Opção não disponível para este tipo de conta.");
                        Console.ResetColor();
                    }
                    break;
                case "0":
                    logado = false;
                    Console.WriteLine("Sessão finalizada. Retornando ao menu principal...");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Opção inválida!");
                    Console.ResetColor();
                    break;
            }
        }
        catch (SaldoInsuficienteException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERRO DE SALDO] {ex.Message}");
            Console.ResetColor();
        }
        catch (OperacaoBancariaException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[REGRA DE NEGÓCIO] {ex.Message}");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[DADO INVÁLIDO] {ex.Message}");
            Console.ResetColor();
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[FORMATO INVÁLIDO] O valor digitado precisa ser numérico.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n[ERRO INESPERADO] {ex.Message}");
            Console.ResetColor();
        }

        if (logado)
        {
            AguardarTecla();
        }
    }
}

static void CadastrarNovaConta(Banco banco)
{
    Console.WriteLine("--- CADASTRO DE NOVA CONTA ---");
    Console.WriteLine("Tipos disponíveis:");
    Console.WriteLine("  1 - Conta Corrente");
    Console.WriteLine("  2 - Conta Poupança");
    Console.WriteLine("  3 - Conta Empresarial");
    Console.Write("Selecione o tipo de conta: ");
    string? tipo = Console.ReadLine()?.Trim();

    int numeroConta = LerInteiro("Digite o número da nova conta: ");
    Console.Write("Digite o nome do titular: ");
    string titular = Console.ReadLine()?.Trim() ?? string.Empty;
    decimal saldoInicial = LerDecimal("Digite o saldo inicial de abertura: R$ ");

    ContaBancaria novaConta = tipo switch
    {
        "1" => new ContaCorrente(numeroConta, titular, saldoInicial),
        "2" => new ContaPoupanca(numeroConta, titular, saldoInicial),
        "3" => new ContaEmpresarial(numeroConta, titular, saldoInicial, LerDecimal("Digite o limite de empréstimo: R$ ")),
        _ => throw new ArgumentException("Tipo de conta selecionado é inválido.")
    };

    banco.AdicionarConta(novaConta);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nSucesso: Conta {novaConta.GetType().Name} nº {numeroConta} cadastrada com sucesso!");
    Console.ResetColor();
}

static void RealizarDeposito(ContaBancaria conta)
{
    Console.WriteLine("--- DEPÓSITO EM CONTA ---");
    decimal valor = LerDecimal("Digite o valor a depositar: R$ ");
    conta.Depositar(valor);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nDepósito de R$ {valor:F2} realizado com sucesso!");
    Console.WriteLine($"Novo saldo da conta {conta.NumeroConta}: R$ {conta.Saldo:F2}");
    Console.ResetColor();
}

static void RealizarSaque(ContaBancaria conta)
{
    Console.WriteLine("--- SAQUE BANCÁRIO ---");
    decimal valor = LerDecimal("Digite o valor a sacar: R$ ");
    conta.Sacar(valor);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nSaque de R$ {valor:F2} realizado com sucesso!");
    Console.WriteLine($"Saldo restante na conta {conta.NumeroConta}: R$ {conta.Saldo:F2}");
    Console.ResetColor();
}

static void RealizarTransferencia(Banco banco, ContaBancaria origem)
{
    Console.WriteLine("--- TRANSFERÊNCIA ENTRE CONTAS ---");
    Console.WriteLine($"Conta de Origem: {origem.NumeroConta} ({origem.Titular})");
    int numDestino = LerInteiro("Digite o número da conta de DESTINO: ");
    ContaBancaria destino = banco.BuscarConta(numDestino);

    decimal valor = LerDecimal("Digite o valor a transferir: R$ ");
    origem.Transferir(valor, destino);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nTransferência de R$ {valor:F2} para conta {destino.NumeroConta} ({destino.Titular}) realizada com sucesso!");
    Console.WriteLine($"Seu Novo Saldo: R$ {origem.Saldo:F2}");
    Console.ResetColor();
}

static void PagarFatura(ContaBancaria conta)
{
    Console.WriteLine("--- PAGAMENTO DE CONTA / FATURA ---");
    Console.Write("Digite a descrição ou código de barras da fatura: ");
    string descricao = Console.ReadLine()?.Trim() ?? string.Empty;

    decimal valor = LerDecimal("Digite o valor da fatura a pagar: R$ ");
    conta.PagarConta(valor, descricao);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nPagamento de '{descricao}' no valor de R$ {valor:F2} realizado com sucesso!");
    Console.WriteLine($"Saldo restante na conta {conta.NumeroConta}: R$ {conta.Saldo:F2}");
    Console.ResetColor();
}

static void AplicarRendimento(ContaPoupanca cp)
{
    Console.WriteLine("--- RENDIMENTO POUPANÇA ---");
    decimal taxa = LerDecimal("Digite o percentual de rendimento a aplicar (ex: 0,5): ");
    decimal rendimento = cp.AplicarRendimento(taxa);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nRendimento de {taxa:F2}% aplicado! Ganho de R$ {rendimento:F2}. Novo Saldo: R$ {cp.Saldo:F2}");
    Console.ResetColor();
}

static void SolicitarEmprestimo(ContaEmpresarial ce)
{
    Console.WriteLine("--- EMPRÉSTIMO EMPRESARIAL ---");
    Console.WriteLine($"Limite atual de empréstimo disponível: R$ {ce.LimiteEmprestimo:F2}");
    decimal valor = LerDecimal("Digite o valor do empréstimo desejado: R$ ");
    ce.RealizarEmprestimo(valor);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\nEmpréstimo de R$ {valor:F2} contratado com sucesso!");
    Console.WriteLine($"Novo Saldo: R$ {ce.Saldo:F2} | Limite restante: R$ {ce.LimiteEmprestimo:F2}");
    Console.ResetColor();
}

static void ListarContas(Banco banco)
{
    Console.WriteLine("=================================================");
    Console.WriteLine("          RELAÇÃO DE TODAS AS CONTAS             ");
    Console.WriteLine("=================================================");
    var contas = banco.ObterTodasContas();

    if (contas.Count == 0)
    {
        Console.WriteLine("Nenhuma conta cadastrada.");
        return;
    }

    foreach (var conta in contas)
    {
        conta.ExibirExtrato();
    }

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"Total de contas no sistema: {contas.Count}");
    Console.WriteLine($"Total sob custódia do banco: R$ {banco.ObterTotalCustodia():F2}");
    Console.ResetColor();
}

static void ExibirRelatorioTributos(Banco banco)
{
    Console.WriteLine("=================================================");
    Console.WriteLine("       RELATÓRIO DE TRIBUTOS (INTERFACE ITributavel)  ");
    Console.WriteLine("=================================================");

    var contas = banco.ObterTodasContas();
    var contasTributaveis = contas.OfType<ITributavel>().ToList();

    if (contasTributaveis.Count == 0)
    {
        Console.WriteLine("Nenhuma conta tributável encontrada.");
        return;
    }

    foreach (var item in contasTributaveis)
    {
        if (item is ContaBancaria cb)
        {
            Console.WriteLine($"Conta: {cb.NumeroConta} | Titular: {cb.Titular} | Tributo Devido: R$ {item.CalcularTributo():F2}");
        }
    }

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine($"Total de Tributos Arrecadados: R$ {banco.CalcularTotalTributos():F2}");
    Console.ResetColor();
}

static int LerInteiro(string prompt)
{
    Console.Write(prompt);
    string? entrada = Console.ReadLine();
    if (!int.TryParse(entrada, out int resultado))
    {
        throw new FormatException("O valor fornecido não é um número inteiro válido.");
    }
    return resultado;
}

static decimal LerDecimal(string prompt)
{
    Console.Write(prompt);
    string? entrada = Console.ReadLine();
    if (!decimal.TryParse(entrada, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal resultado) &&
        !decimal.TryParse(entrada, NumberStyles.Any, CultureInfo.InvariantCulture, out resultado))
    {
        throw new FormatException("O valor fornecido não é um número decimal válido.");
    }
    return resultado;
}

static void LimparTela()
{
    try
    {
        Console.Clear();
    }
    catch (IOException)
    {
        // Ignora caso o ambiente/terminal não suporte limpeza de buffer (ex: saída redirecionada ou terminal integrado)
    }
}

static void AguardarTecla()
{
    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    try
    {
        Console.ReadKey();
    }
    catch (InvalidOperationException)
    {
        Console.ReadLine();
    }
}