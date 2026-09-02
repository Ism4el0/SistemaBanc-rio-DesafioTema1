using System;
using System.Collections.Generic;
using SistemaBancario;

Console.WriteLine("=== SISTEMA BANCÁRIO ===\n");

ContaCorrente cc = new ContaCorrente(101, "João Silva", 500.00m, 6.50m);
ContaPoupanca cp = new ContaPoupanca(202, "Maria Oliveira", 1200.00m);
ContaEmpresarial ce = new ContaEmpresarial(303, "Tech Solutions Ltda", 5000.00m, 10000.00m);

Console.WriteLine(">> TESTE CONTA CORRENTE:");
cc.Depositar(200.00m);
cc.Sacar(100.00m);
cc.ExibirExtrato();

Console.WriteLine();

Console.WriteLine(">> TESTE CONTA POUPANÇA:");
cp.Sacar(200.00m);
cp.AplicarRendimento(0.5m);
cp.ExibirExtrato();

Console.WriteLine();

Console.WriteLine(">> TESTE CONTA EMPRESARIAL:");
ce.Sacar(1000.00m);
ce.RealizarEmprestimo(3000.00m);
ce.ExibirExtrato();

Console.WriteLine();

Console.WriteLine(">> POLIMORFISMO (List<ContaBancaria>):");
List<ContaBancaria> banco = new List<ContaBancaria> { cc, cp, ce };

foreach (var conta in banco)
{
    conta.ExibirExtrato();
}