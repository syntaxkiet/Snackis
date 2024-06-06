using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis.Data;
using Snackis.Models;

namespace Snackis.Pages
{
    public class ReportsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Post> Posts { get; set; }
        public ReportsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGet()
        {
            Posts = await _context.Posts.Where(x => x.isFlagged == 1).ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int deleteId)
        {
            if (deleteId != 0)
            {
                Models.Post postToBeDeleted = await _context.Posts.FindAsync(deleteId);

                if (postToBeDeleted != null)
                {
                    _context.Posts.Remove(postToBeDeleted);
                    await _context.SaveChangesAsync();
                }
            }

            Posts = await _context.Posts.Where(p => p.isFlagged == 1).ToListAsync();
            return Page();
        }
    }
}
