using System.Net.Http.Json;
using CrudFaculdade.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudFaculdade.Web.Pages.Produtos;

public class DeleteModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DeleteModel(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    [BindProperty]
    public ProdutoDto Produto { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("Api");
        var produto = await client.GetFromJsonAsync<ProdutoDto>($"api/produtos/{id}");

        if (produto is null)
            return NotFound();

        Produto = produto;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("Api");
        var resp = await client.DeleteAsync($"api/produtos/{id}");

        if (!resp.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível excluir na API.");
            return Page();
        }

        return RedirectToPage("Index");
    }
}