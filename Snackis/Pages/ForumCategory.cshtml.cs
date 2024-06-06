using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis.Models;
using System.Security.Claims;


namespace Snackis.Pages
{
    public class ForumCategoryModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;
        public Category Category { get; set; }
        [BindProperty]
        public string CategoryName { get; set; }
        [BindProperty]
        public Models.Thread Thread { get; set; }
        public int CurrentCategory { get; set; }
        public List<Models.Thread> Threads { get; set; }

        public ForumCategoryModel(Data.ApplicationDbContext context)
        {
            
            _context = context;
        }

        public async Task OnGetAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                CategoryName = category.Name;
                CurrentCategory = id;
            }
            Threads = await _context.Threads.Where(p => p.CategoryId == id).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            
            Thread.CreatedAt = DateTime.Now;
            Thread.CategoryId = id;
            Thread.Posts = new List<Models.Post>();
            Thread.Title = Helpers.HelperFunctions.SwearFilter(Thread.Title);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Thread.Author = userId;
            _context.Threads.Add(Thread);
            await _context.SaveChangesAsync();

            return RedirectToPage("ForumCategory", new { id });
        }
    }
}
