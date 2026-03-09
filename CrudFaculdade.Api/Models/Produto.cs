using System;
using System.Collections.Generic;

namespace CrudFaculdade.Api.Models;

public partial class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public int Estoque { get; set; }

    public string? Categoria { get; set; }

    public bool Ativo { get; set; }

    public DateTime CriadoEm { get; set; }
}
