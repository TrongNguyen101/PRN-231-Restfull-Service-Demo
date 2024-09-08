using FrontendDemo.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FrontendDemo.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger<IndexModel> _logger;

        public UpdateModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient("BookManagement");
            var httpResponseMessage = await httpClient.GetAsync($"Books/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var book = await JsonSerializer.DeserializeAsync<Book>(contentStream);

                if (book == null)
                {
                    return NotFound();
                }
                Book = book;

                return Page();
            }

            return NotFound();

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var BookJson = new StringContent(
                                JsonSerializer.Serialize(Book),
                                Encoding.UTF8,
                                Application.Json);

            var httpClient = _httpClientFactory.CreateClient("BookManagement");

            using var httpResponseMessage = await httpClient.PutAsync($"Books/{Book.Id}", BookJson);

            httpResponseMessage.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}
