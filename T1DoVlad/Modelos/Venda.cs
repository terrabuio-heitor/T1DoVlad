using System;


//criei essa classe pra servir de modelo e facilitar na geracao de relatorios -- Murilo
namespace projeto_denovo_tp_vladmir.Models {
    public class Venda {
        public string NomeItem { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
        public DateTime DataVenda { get; set; }
    }
}