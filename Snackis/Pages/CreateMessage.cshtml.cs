using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Snackis.Data;
using Snackis.Models;
using System.Security.Claims;

namespace Snackis.Pages
{
    public class CreateMessageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateMessageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PrivateMessage PrivateMessage { get; set; }

        public IActionResult OnGet()
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PrivateMessage = new PrivateMessage
            {
                SenderId = senderId,
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            PrivateMessage.ReceiverId = _context.Users.FirstOrDefault(x => x.Nickname == PrivateMessage.ReceiverId).Id;
            
            if (!ModelState.IsValid)
            {
                return Page();
                
            }
            _context.PrivateMessages.Add(PrivateMessage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
