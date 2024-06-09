using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis.Data;
using Snackis.Models;
using System.Security.Claims;

namespace Snackis.Pages
{
    public class ForumThreadModel : PageModel
    {
        private readonly ApplicationDbContext _context;


        [BindProperty]
        public List<Models.Post> Posts { get; set; }

        [BindProperty]
        public Models.Post Post { get; set; }

        [BindProperty]
        public int CurrentThread { get; set; }

        [BindProperty]
        public string ThreadName { get; set; }

        [BindProperty]
        public List<User> Users { get; set; }

        public ForumThreadModel(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task OnGetAsync(int id)
        {
            CurrentThread = id;
            var thread = await _context.Threads.FindAsync(id);
            if (thread != null)
            {
                ThreadName = thread.Title;
            }
            Posts = await _context.Posts.Where(p => p.ThreadId == id).ToListAsync();
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

            Posts = await _context.Posts.Where(p => p.ThreadId == CurrentThread).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(int editId)
        {
            if (editId != 0)
            {
                Models.Post postToBeEdited = await _context.Posts.FindAsync(editId);
                postToBeEdited.isFlagged = 1;
                _context.Posts.Update(postToBeEdited);
                await _context.SaveChangesAsync();
            }
            Posts = await _context.Posts.Where(p => p.ThreadId == CurrentThread).ToListAsync();

            return RedirectToPage("ForumThread", new { id = CurrentThread });
        }

        public string DisplayUsername(string userId)
        {
            string username = "";
            foreach (User user in Users) {
                if (userId == user.Id)
                {
                    username = user.Nickname;
                }
                return username;
            }

            return username;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Post.ThreadId = CurrentThread;
            Post.CreatedAt = DateTime.Now;
            Post.Author = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Post.Text = Helpers.HelperFunctions.SwearFilter(Post.Text);
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("ForumThread", new { id = CurrentThread });
        }
    }
}
