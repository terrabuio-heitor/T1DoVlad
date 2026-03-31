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