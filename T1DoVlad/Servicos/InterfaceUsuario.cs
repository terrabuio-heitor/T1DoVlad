/*
Alunos
[
 Nome: Heitor Terrabuio
 RA: 252407
 E-mail: heitorterrabuio@gmail.com
],
[
 Nome: Gustavo Campos
 RA: 241734
 E-mail: gustavo.campos.ribeiro@hotmail.com
]
[
 Nome: Luiz Henrique da Silva
 RA: 256732
 E-mail: luiz60828@gmail.com
]
[
 Nome: Murilo Soares Bezerra
 RA: 257013
 E-mail: muri31102006@gmail.com
]

*/


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
                            "Vender", "Gerenciar", "Relatórios"
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
                            .PageSize(20).AddChoices(nomesItens.Append("Voltar")
                            ));
                        if (vender == "Voltar")
                        {
                            Console.WriteLine("Venda cancelada.");
                            break;
                        }
                        int quantidade = AnsiConsole.Ask<int>("Entre com a [Gray]Quantidade[/]:");
                        if (quantidade > 0)
                        {
                            try
                            {
                                LJ.VenderItem(vender, quantidade);
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
                        .PageSize(20).AddChoices("Criar Item", "Editar Item", "Apagar Item", "Voltar"
                        ));
                    switch (op2)
                    {

                        case "Criar Item":
                            CriarItem();
                            break;
                        case "Editar Item":
                            EditarItem();
                            break;
                        case "Apagar Item":
                            ApagarItem();
                            break;
                    }
                    break;
                case "Relatórios":
                    ExibirCabecalho("A Doninha Encantada", true);

                    Console.WriteLine("Vamos checar os relatórios!");
                    var op3 = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("Selecione uma opção:")
                        .PageSize(20).AddChoices("Relatório Estoque", "Relatório de vendas", "Fechamento de caixa", "Voltar"
                        ));
                    switch (op3)
                    {
                        case "Relatório Estoque":
                            MostrarRelatorioEstoque();
                            break;

                        case "Relatório de vendas":
                            LJ.RelatorioVendas();
                            break;

                        case "Fechamento de caixa":
                            LJ.ExibirCaixa();
                            break;

                        case "Voltar":

                            return;
                    }
                    break;
                default:
                    Console.WriteLine("Voltar.");
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

        /*  optei por tirar a opcao VerItem(READ) do Gerenciar e deixar no relatorio de estoque faltam adicionar as opcoes de UPDDATE e DELETE de itens, 
         *  tendo em vista que no proprio arquivo do professor pede pra deixar mostrar a visualizacao dos itens ordenadamente junto dos outros relatorios
         *  usando o LINQ, no modulo 3. -- Murilo
         *  
         *  public void VerItem()
                {
                    var tipoEscolhido = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Qual tipo de item deseja ver?")
                    .AddChoices("Arma", "Pocao", "Voltar"));
                    if (tipoEscolhido != "Voltar")
                    {
                        LJ.VerItensPorTipo(tipoEscolhido);
                    }
                } */

        //public void CriarItem()
        //{
        //    var tipoEscolhido = AnsiConsole.Prompt(
        //    new SelectionPrompt<string>()
        //    .Title("Qual tipo de item deseja ver?")
        //    .AddChoices("Arma", "Pocao", "Voltar"));
        //    if (tipoEscolhido != "Voltar")
        //    {
        //        LJ.AddNovoItem(tipoEscolhido, "", 10, 1);
        //    }
        //}
        public void CriarItem()
        {
            ExibirCabecalho("Novo Item no Estoque", true);
            var tipo = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Qual o [green]tipo[/] do item?")
                    .AddChoices("Arma", "Pocao"));
            var nome = AnsiConsole.Ask<string>("Qual o [blue]nome[/] do item?");
            var preco = AnsiConsole.Prompt(
                new TextPrompt<double>("Qual o [yellow]preço[/] unitário?")
                    .ValidationErrorMessage("[red]Valor inválido![/] Digite um número positivo.")
                    .Validate(p => p >= 0));
            var qtd = AnsiConsole.Prompt(
                new TextPrompt<int>("Quantidade inicial em [white]estoque[/]?")
                    .DefaultValue(1)
                    .Validate(q => q >= 0));
            try
            {
                LJ.AddNovoItem(tipo, nome, preco, qtd);
                AnsiConsole.MarkupLine($"\n[bold green]Sucesso![/] {tipo} '{nome}' adicionada ao estoque.");
                LJ.SalvarDados();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Erro ao salvar:[/] {ex.Message}");
            }
        }
        // funcao de mostrar o relatorio de estoque chamando o metodo do LojaServico que ja organiza os itens por preco, como pedido no documento --Murilo
        public void MostrarRelatorioEstoque()
        {
            ExibirCabecalho("Relatório de Estoque", true);

            var itens = LJ.GerarRelatorioEstoque();

            if (!itens.Any())
            {
                AnsiConsole.MarkupLine("[red]Nenhum item com estoque disponível.[/]");
                return;
            }

            var tabela = new Table();
            tabela.Border(TableBorder.Rounded);
            tabela.AddColumn("[yellow]Tipo[/]");
            tabela.AddColumn("[blue]Nome[/]");
            tabela.AddColumn("[green]Preço[/]");
            tabela.AddColumn("[white]Estoque[/]");

            foreach (var item in itens)
            {
                tabela.AddRow(
                    item.GetType().Name,
                    item.Nome,
                    $"R$ {item.Preco:F2}",
                    item.Estoque.ToString()
                );
            }

            AnsiConsole.Write(tabela);
        }

        //Restante do CRUD

        public void EditarItem()
        {
            ExibirCabecalho("Atualizar Suprimentos", true);
            var nomes = LJ.ObterNomesDosItens();

            if (!nomes.Any())
            {
                AnsiConsole.MarkupLine("[yellow]O estoque está vazio![/]");
                return;
            }

            var itemSelecionado = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Selecione o item para [blue]modificar[/]:")
                .AddChoices(nomes));

            var acao = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("O que deseja fazer?")
                .AddChoices("Atualizar Preço", "Repor Estoque", "Cancelar"));

            try
            {
                switch (acao)
                {
                    case "Atualizar Preço":
                        double novoPreco = AnsiConsole.Ask<double>("Digite o [yellow]novo preço[/]:");
                        LJ.AtualizarPreco(itemSelecionado, novoPreco); // Chama o serviço [cite: 47]
                        AnsiConsole.MarkupLine("[green]Preço atualizado com sucesso![/]");
                        break;

                    case "Repor Estoque":
                        int qtd = AnsiConsole.Ask<int>("Quantidade para [white]adicionar[/]:");
                        LJ.ReporEstoque(itemSelecionado, qtd); // Requisito do Módulo 1 [cite: 29]
                        AnsiConsole.MarkupLine("[green]Estoque abastecido![/]");
                        break;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Erro:[/] {ex.Message}"); // Defesa com try-catch [cite: 45]
            }
        }

        public void ApagarItem()
        {
            ExibirCabecalho("Remover do Inventário", true);
            var nomes = LJ.ObterNomesDosItens();

            var itemParaRemover = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[red]CUIDADO:[/] Qual item deseja remover permanentemente?")
                .AddChoices(nomes));

            if (AnsiConsole.Confirm($"Deseja mesmo excluir [bold]{itemParaRemover}[/]?"))
            {
                try
                {
                    LJ.RemoverItem(itemParaRemover);
                    AnsiConsole.MarkupLine("[yellow]Item removido dos registros![/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Erro ao remover:[/] {ex.Message}");
                }
            }
        }

    }
}