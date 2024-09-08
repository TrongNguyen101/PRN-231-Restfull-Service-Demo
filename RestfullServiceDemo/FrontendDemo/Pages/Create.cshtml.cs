using System.Text;
using System.Text.Json;
using FrontendDemo.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;
namespace FrontendDemo.Pages
{
    public class CreateModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var httpClient = _httpClientFactory.CreateClient();

            var data = new StringContent(
                JsonSerializer.Serialize(Book),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage = await httpClient.PostAsync("https://localhost:7261/api/Books", data);

            httpResponseMessage.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}
