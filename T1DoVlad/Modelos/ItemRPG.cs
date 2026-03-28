using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_denovo_tp_vladmir.Models {
    public abstract class ItemRPG {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int Estoque { get; set; }
        public string Tipo { get; protected set; }

        protected ItemRPG(string nome, double preco, int estoque) {
            Nome = nome;
            Preco = preco;
            Estoque = estoque;
        }

    }

}