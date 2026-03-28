using projeto_denovo_tp_vladmir.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ja criado a herenca do pocao e do arma pro ItemRPG

namespace projeto_denovo_tp_vladmir.Models {
    public class Pocao : ItemRPG {
        public Pocao(string nome, double preco, int estoque)
            : base(nome, preco, estoque) {
            Tipo = "Pocao";
        }
    }
}
