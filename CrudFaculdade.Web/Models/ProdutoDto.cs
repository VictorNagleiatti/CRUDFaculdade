namespace CrudFaculdade.Web.Models;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public string? Categoria { get; set; }
    public bool Ativo { get; set; }
}