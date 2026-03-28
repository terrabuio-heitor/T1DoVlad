using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using projeto_denovo_tp_vladmir.Models;
using T1DoVlad.Servicos;


using Spectre.Console;//extensão para melhorar o CLI, deixando ele mais bonito e interativo.


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
                    //LojaServico LJ = new LojaServico();//puxo a classe
                    ExibirCabecalho("A Doninha Encantada", false);
                    AnsiConsole.Write(new Panel("Bem vindo ao sistema novo da loja...")
                        .Header("[bold yellow]Status[/]")
                        .Border(BoxBorder.Rounded)
                        .BorderStyle(new Style(Color.Blue))
                    );
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
                            ExibirCabecalho("A Doninha Encantada", true);
                            Console.WriteLine("\nVamos vender um item!");
                            //Thread.Sleep(1000);
                            //Console.WriteLine("Qual item você deseja vender?");

                            //var nomesItens = LJ.ObterNomesDosItens();
                            int CORRIGIRcomPRIORIDADE = 0;
                            
                            //aqui tem que ser a função de obter os itens do banco, mas como não fiz ainda, deixei assim para testar o loading bar. 

                            /*
                            loadingBar(nomesItens.Count);
                            if (nomesItens.Count == 0)
                            {
                                AnsiConsole.MarkupLine($"Você selecionou: [yellow]{op}[/]");
                                Console.WriteLine("A loja está vazia! Não há nada para vender.");
                                break;
                            }
                            else
                            {
                                var lista = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                    .Title("Selecione um item para vender:")
                                    .PageSize(20).AddChoices(nomesItens
                                    ));
                            }

                            */

                            break;
                        case "Gerenciar":
                            ExibirCabecalho("A Doninha Encantada", true);
                            Console.WriteLine("Vamos criar um item para a loja!");
                            Console.WriteLine("Ou deseja ver os que já existem?");
                            var op2 = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                .Title("Selecione uma opção:")
                                .PageSize(20).AddChoices("Criar Item", "Ver Itens", "Editar Item", "Apagar Item"
                                ));
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                    var sair = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("\n\n\nDeseja [red]SAIR[/]?")
                    .PageSize(3)
                    .AddChoices(new[] {
                        "Sim", "Não"
                    }));
                    if (sair == "Sim")
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
        public static void ExibirCabecalho(string titulo, bool p)
        {
            Console.Clear();
            if (p) { AnsiConsole.MarkupLine($"[bold blue]{titulo}[/]"); } else { AnsiConsole.Write(new FigletText(titulo).Justify(Justify.Left).Color(Spectre.Console.Color.Gold1)); }
        }
        static void loadingBar(int time)
        {
            AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Sincronizando arquivos...", ctx =>
            {
                Thread.Sleep(time * 250);
                AnsiConsole.MarkupLine("Carregando produtos [green]OK[/]");
            });
        }
    }
}
//var fruta = AnsiConsole.Prompt(
//new SelectionPrompt<string>()
//.Title("Qual sua [green]fruta[/] favorita?")
//.PageSize(10)
//.AddChoices(new[] {
//    "Maçã", "Banana", "Laranja",
//    "Morango", "Kiwi", "Abacaxi"
//}));
//AnsiConsole.MarkupLine($"Você selecionou: [green]{fruta}[/]");