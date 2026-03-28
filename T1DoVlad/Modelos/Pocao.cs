namespace projeto_denovo_tp_vladmir.Models
{
    public class Pocao : ItemRPG
    {
        public Pocao(string nome, double preco, int estoque)
            : base(nome, preco, estoque) { }

        public override string ExibirDetalhes()
        {
            return $"[POÇÃO] {Nome} | R$ {Preco} | Estoque: {Estoque}";
        }
    }
}