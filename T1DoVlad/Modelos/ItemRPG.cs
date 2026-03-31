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