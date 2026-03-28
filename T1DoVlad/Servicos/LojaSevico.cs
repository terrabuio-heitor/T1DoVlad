using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using projeto_denovo_tp_vladmir.Models;

namespace T1DoVlad.Servicos
{
    [Serializable]
    internal class LojaSevico
    {
        public List<Arma> armas = new List<Arma>();
        private List<Pocao> pocoes = new List<Pocao>();
        //private 

        static ItemRPG CriarItem()
        {
            string nome = LerNome();
            double preco = LerPreco();
            int estoque = LerEstoque();
            string tipo = LerTipo();

            if (tipo == "arma")
            {
                return new Arma(nome, preco, estoque);
            }
            else if (tipo == "pocao")
            {
                return new Pocao(nome, preco, estoque);
            }
            else
            {
                throw new Exception("Tipo inválido.");
            }
        }

        public LojaSevico()
        {
            CarregarItensPadrao();
        }

        private void CarregarItensPadrao()
        {
            // Adicionando Armas
            armas.Add(new Arma("Espada de Ferro", 150.0, 5));
            armas.Add(new Arma("Arco de Caçador", 120.0, 3));
            armas.Add(new Arma("Cajado de Aprendiz", 200.0, 2));

            // Adicionando Poções
            pocoes.Add(new Pocao("Poção de Vida P", 50.0, 10));
            pocoes.Add(new Pocao("Poção de Mana P", 60.0, 8));
            pocoes.Add(new Pocao("Elixir da Força", 500.0, 1));
        }

        void AddInventario(ItemRPG item)
        {
            if (item is Arma arma)
            {
                armas.Add(arma);
            }
            else if (item is Pocao pocao)
            {
                pocoes.Add(pocao);
            }
        }

        static string LerNome()
        {
            Console.WriteLine("Qual o nome do item?");
            return Console.ReadLine();
        }

        static double LerPreco()
        {
            Console.WriteLine("Qual o valor do item?");
            return double.Parse(Console.ReadLine());
        }

        static int LerEstoque()
        {
            Console.WriteLine("Quanto em estoque?");
            return int.Parse(Console.ReadLine());
        }

        static string LerTipo()
        {
            Console.WriteLine("Tipo do item? (arma/pocao)");
            return Console.ReadLine().ToLower();
        }

        public List<string> ObterNomesDosItens()
        {
            // Pegamos os nomes da lista de armas
            var nomesArmas = armas.Select(a => a.Nome);

            // Pegamos os nomes da lista de poções
            var nomesPocoes = pocoes.Select(p => p.Nome);

            // Juntamos tudo em uma lista de strings e retornamos
            return nomesArmas.Concat(nomesPocoes).ToList();
        }
        static void ExibirItem(ItemRPG item)
        {
            Console.WriteLine($"Nome: {item.Nome}");
            Console.WriteLine($"Preço: {item.Preco}");
            Console.WriteLine($"Estoque: {item.Estoque}");
            Console.WriteLine($"Tipo: {item.Tipo}");
        }
    }
}