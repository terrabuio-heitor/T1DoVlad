using System;
using System.Collections.Generic;
using System.Linq;
using projeto_denovo_tp_vladmir.Models;

namespace T1DoVlad.Servicos
{
    [Serializable]
    internal class LojaServico
    {
        private List<ItemRPG> itens = new List<ItemRPG>();
        private List<string> historicoVendas = new List<string>();
        private double caixa = 0;

        public LojaServico()
        {
            CarregarItensPadrao();
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

        public void VenderItem(string nome, int quantidade)
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
    }
}