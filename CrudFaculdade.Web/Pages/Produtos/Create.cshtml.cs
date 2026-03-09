using System.Net.Http.Json;
using CrudFaculdade.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrudFaculdade.Web.Pages.Produtos;

public class CreateModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateModel(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;

    [BindProperty]
    public ProdutoDto Produto { get; set; } = new() { Ativo = true };

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var client = _httpClientFactory.CreateClient("Api");
        var resp = await client.PostAsJsonAsync("api/produtos", Produto);

        if (!resp.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível salvar na API.");
            return Page();
        }

        return RedirectToPage("Index");
    }
}