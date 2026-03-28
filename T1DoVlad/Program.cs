//using projeto_denovo_tp_vladmir.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using projeto_denovo_tp_vladmir.Models;
using T1DoVlad.Servicos;


using Spectre.Console;
using System.ComponentModel;//extensão para melhorar o CLI, deixando ele mais bonito e interativo.

//ja criei e configurei o banco de dados usando o SQLite no DatabaseConfig.cs.
//optei por antes de inserir os dados na tabela do banco, fazer as funcoes de leitura e criacao de item de acordo com os parametros do professor.

namespace projeto_denovo_tp_vladmir
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool loop = true;
            while (loop)
            {
                try
                {
                    LojaSevico LJ = new LojaSevico();//puxo a classe

                    Console.WriteLine("Bem vindo ao sistema novo da loja: A Doninha Encantada ");
                    Thread.Sleep(500);
                    Console.WriteLine("O que deseja fazer? ");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Deseja VENDER um item ou Deseja GERENCIAR os itens?");
                    Thread.Sleep(500);
                    var op = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("\nO que deseja fazer?")
                            .PageSize(10)
                            .AddChoices(new[] {
                            "Vender", "Gerenciar"
                            }));


                    switch (op)
                    {
                        case "Vender":
                            Console.WriteLine("\nVamos vender um item!");
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine("Qual item você deseja vender?");

                            var nomesItens = LJ.ObterNomesDosItens();
                            if (nomesItens.Count == 0)
                            {
                                AnsiConsole.MarkupLine($"Você selecionou: [yellow]{op}[/]");
                                Console.WriteLine("A loja está vazia! Não há nada para vender.");
                                break;
                            }

                            var lista = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                .Title("Selecione um item para vender:")
                                .PageSize(20).AddChoices(nomesItens
                                ));
                            break;
                        case "Gerenciar":
                            Console.WriteLine("Vamos criar um item para a loja!");
                            Console.WriteLine("Ou deseja ver os que já existem?");



                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }

                    //var fruta = AnsiConsole.Prompt(
                    //new SelectionPrompt<string>()
                    //.Title("Qual sua [green]fruta[/] favorita?")
                    //.PageSize(10)
                    //.AddChoices(new[] {
                    //    "Maçã", "Banana", "Laranja",
                    //    "Morango", "Kiwi", "Abacaxi"
                    //}));
                    //AnsiConsole.MarkupLine($"Você selecionou: {fruta}[/]");
                    var sair = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("\n\n\n\n\nDeseja [red]SAIR[/]?")
                    .PageSize(3)
                    .AddChoices(new[] {
                        "Sim", "Não"
                    }));
                    if(sair == "Sim")
                    {
                        loop = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}