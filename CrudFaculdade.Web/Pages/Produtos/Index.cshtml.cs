using System.Net.Http.Json;
using CrudFaculdade.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudFaculdade.Web.Pages.Produtos;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public List<ProdutoDto> Produtos { get; set; } = new();

    // vem da URL: /Produtos?Q=mouse&Categoria=GPU
    [BindProperty(SupportsGet = true)]
    public string? Q { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Categoria { get; set; }

    public IndexModel(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("Api");

        var url = "api/produtos";
        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(Q))
            parts.Add($"q={Uri.EscapeDataString(Q)}");

        if (!string.IsNullOrWhiteSpace(Categoria))
            parts.Add($"categoria={Uri.EscapeDataString(Categoria)}");

        if (parts.Count > 0)
            url += "?" + string.Join("&", parts);

        Produtos = await client.GetFromJsonAsync<List<ProdutoDto>>(url) ?? new();
    }
}