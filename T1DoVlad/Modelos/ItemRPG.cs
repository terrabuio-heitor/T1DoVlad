using System;
using System.Runtime.InteropServices;

namespace projeto_denovo_tp_vladmir.Models
{
    public abstract class ItemRPG
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        private double preco;
        public double Preco
        {
            get => preco;
            set
            {
                if (value < 0)
                    throw new Exception("Preço não pode ser negativo.");
                preco = value;
            }
        }

        private int estoque;
        public int Estoque
        {
            get => estoque;
            set
            {
                if (value < 0)
                    throw new Exception("Estoque não pode ser negativo.");
                estoque = value;
            }
        }

        public string Tipo { get; protected set; }

        protected ItemRPG(string nome, double preco, int estoque)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome inválido.");

            Nome = nome;
            Preco = preco;
            Estoque = estoque;
        }

        public abstract string ExibirDetalhes();
    }
}