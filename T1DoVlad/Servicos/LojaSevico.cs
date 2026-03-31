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
using System.Linq;
using Newtonsoft.Json; // Se puder usar NuGet, essa é a melhor
using System.IO;


namespace T1DoVlad.Servicos
{

    [Serializable]
    internal class LojaServico
    {
        private readonly string caminhoEstoque = "estoque.json";
        private readonly string DadosVenda = "dadosVenda.json";
        private List<ItemRPG> itens = new List<ItemRPG>();
        private List<Venda> historicoVendas = new List<Venda>(); //optei por transformar o historico vendas em um objeto para facilitar o uso correto das informacoes com uso do LINQ -- Murilo
        private double caixa = 0;


        public LojaServico()
        {
            CarregarDados();
            CarregarDadosVenda();
        }

        public List<string> ObterNomesDosItens()
        {
            // Usamos LINQ para pegar apenas o Nome de cada item e converter para uma Lista --Heitor
            return itens.Select(i => i.Nome).ToList();
        }

        public void SalvarDados()
        { // Usei o Json.NET para serializar a lista de itens, incluindo o tipo de cada item, para facilitar a desserialização depois --Heitor
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string json = JsonConvert.SerializeObject(itens, Formatting.Indented, settings);
            File.WriteAllText(caminhoEstoque, json);
        }
        public void SalvarDadosVenda()
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string json = JsonConvert.SerializeObject(historicoVendas, Formatting.Indented, settings);
            File.WriteAllText(DadosVenda, json);
        }
        public void CarregarDados()
        {//Ele testa se o arquivo existe, se existir ele lê o conteúdo e desserializa para a lista de itens, se não existir ele carrega os itens padrão e salva no arquivo para futuras execuções --Heitor
            if (File.Exists(caminhoEstoque))
            {
                string json = File.ReadAllText(caminhoEstoque);
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                itens = JsonConvert.DeserializeObject<List<ItemRPG>>(json, settings) ?? new List<ItemRPG>();
            }
            else
            {
                CarregarItensPadrao();
                SalvarDados();
            }
        }

        public void CarregarDadosVenda()
        {
            if (File.Exists(DadosVenda))
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                string json = File.ReadAllText(DadosVenda);
                historicoVendas = JsonConvert.DeserializeObject<List<Venda>>(json, settings) ?? new List<Venda>();
            }
            else
            {
                historicoVendas = new List<Venda>();
            }
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


        //metodo a baixo lista os items e os organiza do mais caro pro mais barato, alem de mostrar apenas itens com ao menos 1 unidade em estoque --Murilo
        public List<ItemRPG> GerarRelatorioEstoque()
        {
            return itens
                .Where(i => i.Estoque > 0)
                .OrderByDescending(i => i.Preco)
                .ToList();
        }

        //metodo a baixo retorna as vendas ordenado pela data
        public List<Venda> GerarRelatorioVendas()
        {
            return historicoVendas
                .OrderByDescending(v => v.DataVenda)
                .ToList();
        }
        //metodo a baixo soma o total de vendas
        public double GerarFechamentoCaixa()
        {
            return historicoVendas.Sum(v => v.ValorTotal);
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

            //alterei o modo que o historicoVendas funciona pra criar o objeto Venda, para que toda vez que uma venda seja realizada, ele ja adicione pra lista com todos os dados --Murilo
            historicoVendas.Add(new Venda
            {
                NomeItem = item.Nome,
                Quantidade = quantidade,
                ValorUnitario = item.Preco,
                ValorTotal = total,
                DataVenda = DateTime.Now
            });
            Console.WriteLine("Venda realizada com sucesso!");
            AnsiConsole.MarkupLine($"[green] Total da venda: R$ {total:F2} [/]");
            SalvarDadosVenda();
            SalvarDados();
            return (decimal)total;

        }


        //agora esse metodo usa a funcao GerarRelatoriovendas(); e exibe as vendas formatadas --Murilo
        public void RelatorioVendas()
        {
            var vendas = GerarRelatorioVendas();

            if (!vendas.Any())
            {
                Console.WriteLine("Nenhuma venda realizada.");
                return;
            }

            foreach (var v in vendas)
            {
                Console.WriteLine($"{v.NomeItem} - {v.Quantidade}x - unitário: R$ {v.ValorUnitario:F2} - total: R$ {v.ValorTotal:F2} - data: {v.DataVenda:dd/MM/yyyy HH:mm}");
            }
        }
        //agora o total é calculado automaticamente pelo histórico de vendas, sem depender da variável valorTotalVendas
        public void ExibirCaixa()
        {
            double total = GerarFechamentoCaixa();
            Console.WriteLine($"Total em caixa: R$ {total:F2}");
        }

        /*
           removido valorTotalVendas e totalVendasAdd()
           agora o total é calculado automaticamente via LINQ --Murilo */

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

        public void AddNovoItem(string s, string n, double p, int q)
        {
            if (s.Equals("Arma", StringComparison.OrdinalIgnoreCase))
                itens.Add(new Arma(n, p, q));
            else if (s.Equals("Pocao", StringComparison.OrdinalIgnoreCase))
                itens.Add(new Pocao(n, p, q));
            else
                throw new Exception("Tipo de item inválido. Use 'Arma' ou 'Pocao'.");
        }

        //restante do CRUD

        // UPDATE: Repor Estoque
        public void ReporEstoque(string nome, int quantidade)
        {
            var item = itens.FirstOrDefault(i => i.Nome.ToLower() == nome.ToLower());
            if (item == null) throw new Exception("Item não encontrado!");

            item.Estoque += quantidade; // A propriedade já tem validação contra negativos 
            SalvarDados();
        }

        // UPDATE: Atualizar Preço
        public void AtualizarPreco(string nome, double novoPreco)
        {
            var item = itens.FirstOrDefault(i => i.Nome.ToLower() == nome.ToLower());
            if (item == null) throw new Exception("Item não encontrado!");

            item.Preco = novoPreco;
            SalvarDados();
        }

        // DELETE: Remover Item
        public void RemoverItem(string nome)
        {
            var item = itens.FirstOrDefault(i => i.Nome.ToLower() == nome.ToLower());
            if (item == null) throw new Exception("Item não encontrado!");

            itens.Remove(item);
            SalvarDados();
        }

    }
}