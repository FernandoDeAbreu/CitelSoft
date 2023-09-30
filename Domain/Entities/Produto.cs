namespace Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string CodBarras { get; set; }
        public string Referencia { get; set; }
        public byte? Imagem { get; set; }
        public float ValorVenda { get; set; }
        public float ValorCusto { get; set; }
        public float Estoque { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}