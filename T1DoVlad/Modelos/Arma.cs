/*
Alunos
[
 Nome: Heitor Terrabuio
 RA: 252407
 E-mail: heitorterrabuio@gmail.com
],
[
 Nome: Gustavo Campos
 RA: RA do Aluno 2
 E-mail: E-mail do aluno 2
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


namespace projeto_denovo_tp_vladmir.Models
{
    public class Arma : ItemRPG
    {
        public Arma(string nome, double preco, int estoque)
            : base(nome, preco, estoque) { }

        public override string ExibirDetalhes()
        {
            return $"[ARMA] {Nome} | R$ {Preco} | Estoque: {Estoque}";
        }
    }
}