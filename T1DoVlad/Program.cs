//using projeto_denovo_tp_vladmir.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using projeto_denovo_tp_vladmir.Models;
using T1DoVlad.Servicos;

//ja criei e configurei o banco de dados usando o SQLite no DatabaseConfig.cs.
//optei por antes de inserir os dados na tabela do banco, fazer as funcoes de leitura e criacao de item de acordo com os parametros do professor.

namespace projeto_denovo_tp_vladmir {
    internal class Program {
        static void Main(string[] args) {
            //ItemRPG item = CriarItem();
            //ExibirItem(item);

            Console.WriteLine("Bem vindo ao sistema novo da loja: A Doninha Encantada ");


            Console.ReadLine();
        }

        static ItemRPG CriarItem() {
            string nome = LerNome();
            double preco = LerPreco();
            int estoque = LerEstoque();
            string tipo = LerTipo();

            if (tipo == "arma") {
                return new Arma(nome, preco, estoque);
            }
            else if (tipo == "pocao") {
                return new Pocao(nome, preco, estoque);
            }
            else {
                throw new Exception("Tipo inválido.");
            }
        }

        static string LerNome() {
            Console.WriteLine("Qual o nome do item?");
            return Console.ReadLine();
        }

        static double LerPreco() {
            Console.WriteLine("Qual o valor do item?");
            return double.Parse(Console.ReadLine());
        }

        static int LerEstoque() {
            Console.WriteLine("Quanto em estoque?");
            return int.Parse(Console.ReadLine());
        }

        static string LerTipo() {
            Console.WriteLine("Tipo do item? (arma/pocao)");
            return Console.ReadLine().ToLower();
        }

        static void ExibirItem(ItemRPG item) {
            Console.WriteLine($"Nome: {item.Nome}");
            Console.WriteLine($"Preço: {item.Preco}");
            Console.WriteLine($"Estoque: {item.Estoque}");
            Console.WriteLine($"Tipo: {item.Tipo}");
        }
    }
}