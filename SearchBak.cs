using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAPI.Pages.Search
{
    public class SearchBak : PageModel
    {
        private readonly ForumContext _context;

        public SearchBak(ForumContext context)
        {
            _context = context;
        }

        public string Query { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Thread> Threads { get; set; } = new List<Thread>();
        public List<Post> Posts { get; set; } = new List<Post>();

        public async Task OnGetAsync(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                Query = query;

                Categories = await _context.Categories
                    .Where(c => c.Name.Contains(query) || c.Description.Contains(query))
                    .ToListAsync();

                Threads = await _context.Threads
                    .Where(t => t.Title.Contains(query) || t.Author.Contains(query))
                    .Include(t => t.Category)
                    .ToListAsync();

                Posts = await _context.Posts
                    .Where(p => p.Text.Contains(query) || p.Author.Contains(query))
                    .Include(p => p.Thread)
                    .ToListAsync();
            }
        }
    }
}
