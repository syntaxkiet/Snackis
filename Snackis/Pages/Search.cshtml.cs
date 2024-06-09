using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Snackis.Models;
using Microsoft.Extensions.Configuration;

namespace Snackis.Pages
{
    public class SearchModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public SearchModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl");
        }

        public string Query { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Models.Thread> Threads { get; set; } = new List<Models.Thread>();
        public List<Post> Posts { get; set; } = new List<Post>();

        public async Task OnGetAsync(string query)
        {
            Query = query;

            if (!string.IsNullOrEmpty(query))
            {
                var categoriesResponse = await _httpClient.GetStringAsync($"{_apiBaseUrl}/api/Categories/{query}");
                Categories = JsonConvert.DeserializeObject<List<Category>>(categoriesResponse);

                var threadsResponse = await _httpClient.GetStringAsync($"{_apiBaseUrl}/api/Threads/{query}");
                Threads = JsonConvert.DeserializeObject<List<Models.Thread>>(threadsResponse);

                var postsResponse = await _httpClient.GetStringAsync($"{_apiBaseUrl}/api/Posts/{query}");
                Posts = JsonConvert.DeserializeObject<List<Post>>(postsResponse);
            }
        }

        public string GetThreadName(int threadId)
        {
            var thread = Threads.FirstOrDefault(t => t.Id == threadId);
            return thread?.Title ?? "Unknown";
        }
    }
}