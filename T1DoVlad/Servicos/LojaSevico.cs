using projeto_denovo_tp_vladmir.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace T1DoVlad.Servicos
{

    [Serializable]
    internal class LojaServico
    {
        public decimal valorTotalVendas { get; private set; }//variável global para armazenar o valor total das vendas, para facilitar a exibição do caixa no final do dia --Heitor
        private List<ItemRPG> itens = new List<ItemRPG>();
        private List<string> historicoVendas = new List<string>();
        private double caixa = 0;

        
        public LojaServico()
        {
            CarregarItensPadrao();
        }

        public List<string> ObterNomesDosItens()
        {
            // Usamos LINQ para pegar apenas o Nome de cada item e converter para uma Lista --Heitor
            return itens.Select(i => i.Nome).ToList();
        }

        private void CarregarItensPadrao()
        {
            itens.Add(new Arma("Espada de Ferro", 150, 5));
            itens.Add(new Arma("Arco de Caçador", 120, 3));
            itens.Add(new Arma("Cajado de Aprendiz", 200, 2));

            itens.Add(new Pocao("Poção de Vida", 50, 10));
            itens.Add(new Pocao("Poção de Mana", 60, 8));
            itens.Add(new Pocao("Elixir da Força", 500, 1));
        }

        public void AdicionarItem(ItemRPG item)
        {
            itens.Add(item);
        }

        public void ListarEstoque()
        {
            var lista = itens
                .Where(i => i.Estoque > 0)
                .OrderByDescending(i => i.Preco);

            foreach (var item in lista)
            {
                Console.WriteLine(item.ExibirDetalhes());
            }
        }

        public decimal VenderItem(string nome, int quantidade)
        {
            var item = itens.FirstOrDefault(i => i.Nome.ToLower() == nome.ToLower());

            if (item == null)
                throw new Exception("Item não encontrado.");
            if (quantidade > item.Estoque)
                throw new Exception("Estoque insuficiente.");

            double total = item.Preco * quantidade;

            item.Estoque -= quantidade;
            caixa += total;

            historicoVendas.Add($"{item.Nome} - {quantidade}x = R$ {total}");
            Console.WriteLine("Venda realizada com sucesso!");
            AnsiConsole.MarkupLine($"[green] Total da venda: R$ {total:F2} [/]");
            return (decimal)total;

        }

        public void RelatorioVendas()
        {
            foreach (var v in historicoVendas)
            {
                Console.WriteLine(v);
            }
        }

        public void ExibirCaixa()
        {
            Console.WriteLine($"Total em caixa: R$ {caixa}");
        }

        //Função para achar os Itens por Tipo, fazer o select melhor --Heitor
        public void VerItensPorTipo(string tipo)
        {
            InterfaceUsuario UI = new InterfaceUsuario();
            var itensFiltrados = itens.Where(i => i.GetType().Name.Equals(tipo, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!itensFiltrados.Any())
            {
                Console.WriteLine($"Nenhum item do tipo '{tipo}' encontrado no estoque.");
                return;
            }

            //Console.WriteLine($"--- Listando: {tipo.ToUpper()} ---");
            //itensFiltrados.ForEach(item => Console.WriteLine(item.ExibirDetalhes()));

            UI.loadingBar(itensFiltrados.Count);

            var tabela = new Table();
            tabela.Border(TableBorder.Rounded);
            tabela.AddColumn("[yellow]Tipo[/]");
            tabela.AddColumn("[blue]Nome[/]");
            tabela.AddColumn("[green]Preço[/]");
            tabela.AddColumn("[white]Estoque[/]");

            foreach (var item in itensFiltrados)
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
        public void totalVendasAdd(decimal valor)
        {
            valorTotalVendas += valor;
        }
    }
}