using FrontendDemo.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FrontendDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IEnumerable<Book> Books { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7261/api/Books"){};
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                Books = await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(contentStream);
            }

        }
    }
}
