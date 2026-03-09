using CrudFaculdade.Api.Data;
using CrudFaculdade.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudFaculdade.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProdutosController(AppDbContext db) => _db = db;

    // GET: api/produtos?q=mouse&categoria=GPU
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> Get([FromQuery] string? q, [FromQuery] string? categoria)
    {
        var query = _db.Produtos.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            query = query.Where(p => p.Nome.Contains(q));
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            categoria = categoria.Trim();
            query = query.Where(p => p.Categoria != null && p.Categoria.Contains(categoria));
        }

        var lista = await query
            .OrderByDescending(p => p.Id)
            .ToListAsync();

        return Ok(lista);
    }

    // GET: api/produtos/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Produto>> GetById(int id)
    {
        var produto = await _db.Produtos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        return produto is null ? NotFound() : Ok(produto);
    }

    // POST: api/produtos
    [HttpPost]
    public async Task<ActionResult<Produto>> Post([FromBody] Produto produto)
    {
        produto.Id = 0; // garante insert
        await _db.Produtos.AddAsync(produto);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    // PUT: api/produtos/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] Produto dto)
    {
        var produto = await _db.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        if (produto is null) return NotFound();

        produto.Nome = dto.Nome;
        produto.Preco = dto.Preco;
        produto.Estoque = dto.Estoque;
        produto.Categoria = dto.Categoria;
        produto.Ativo = dto.Ativo;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/produtos/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _db.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        if (produto is null) return NotFound();

        _db.Produtos.Remove(produto);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}