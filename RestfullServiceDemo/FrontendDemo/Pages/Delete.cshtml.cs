using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestfullServiceDemo.DataContext;
using RestfullServiceDemo.Model;
using static System.Net.Mime.MediaTypeNames;

namespace FrontendDemo.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger<IndexModel> _logger;

        public DeleteModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var BookJson = new StringContent(
                                JsonSerializer.Serialize(Book),
                                Encoding.UTF8,
                                Application.Json);
            var httpClient = _httpClientFactory.CreateClient("BookManagement");
            using var httpResponseMessage = await httpClient.DeleteAsync($"Books/{id}");

            return RedirectToPage("./Index");
        }
    }
}
