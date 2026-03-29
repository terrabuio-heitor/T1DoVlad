using projeto_denovo_tp_vladmir.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Classe criada para lidar com a interface do usuário, ou seja, toda a parte de exibição e interação,
//deixando a classe Program apenas para rodar o sistema --Heitor


namespace T1DoVlad.Servicos
{
    internal class InterfaceUsuario
    {
        LojaServico LJ = new LojaServico();
        public InterfaceUsuario() { }

        public void ExibirCabecalho(string titulo, bool p)
        {
            Console.Clear();
            if (p) { AnsiConsole.MarkupLine($"[bold blue]{titulo}[/]"); } else { AnsiConsole.Write(new FigletText(titulo).Justify(Justify.Left).Color(Spectre.Console.Color.Gold1)); }
        }
        public void loadingBar(int time)
        {
            AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .Start("Sincronizando arquivos...", ctx =>
            {
                Thread.Sleep(time * 250);
                AnsiConsole.MarkupLine("Carregando produtos [green]OK[/]");
            });
        }

        public void ExibirMenu()
        {
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

                    var nomesItens = LJ.ObterNomesDosItens();
                    //int CORRIGIRcomPRIORIDADE = 0;

                    //aqui tem que ser a função de obter os itens do banco, mas como não fiz ainda, deixei assim para testar o loading bar. 

                    loadingBar(nomesItens.Count);
                    if (nomesItens.Count == 0)
                    {
                        AnsiConsole.MarkupLine($"Você selecionou: [yellow]{op}[/]");
                        Console.WriteLine("A loja está vazia! Não há nada para vender.");
                        break;
                    }
                    else
                    {
                        var vender = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .Title("Selecione um item para vender:")
                            .PageSize(20).AddChoices(nomesItens
                            ));
                        int quantidade = AnsiConsole.Ask<int>("Entre com a [Gray]Quantidade[/]:");
                        if (quantidade > 0)
                        {
                            try
                            {
                                decimal valor = LJ.VenderItem(vender, quantidade);
                                LJ.totalVendasAdd(valor);
                            }
                            catch (Exception ex)
                            {
                                AnsiConsole.MarkupLine($"[red]Erro na venda: {ex.Message}[/]");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Venda errada");
                        }
                        break;
                    }
                case "Gerenciar":
                    ExibirCabecalho("A Doninha Encantada", true);
                    Console.WriteLine("Vamos criar um item para a loja!");
                    Console.WriteLine("Ou deseja ver os que já existem?");
                    var op2 = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("Selecione uma opção:")
                        .PageSize(20).AddChoices("Criar Item", "Ver Itens", "Editar Item", "Apagar Item"
                        ));
                    switch (op2){
                        
                        case "Criar Item":
                            break;
                        case "Ver Itens":
                            VerItem();
                            break;
                        case "Editar Item":
                            break;
                        case "Apagar Item":
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
        public bool ExibirSair()
        {
            var sair = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("\n\n\nDeseja [red]SAIR[/]?")
                    .PageSize(3)
                    .AddChoices(new[] {
                        "Não", "Sim"
                    }));
            if (sair == "Sim")
            {
                return false;
            }
            return true;
        }
        //Vou fazer o CRUD .--Heitor

        public void VerItem()
        {
            var tipoEscolhido = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Qual tipo de item deseja ver?")
            .AddChoices("Arma", "Pocao", "Voltar"));
            if (tipoEscolhido != "Voltar")
            {
                LJ.VerItensPorTipo(tipoEscolhido);
            }
        }
    }
}
