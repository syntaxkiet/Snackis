using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Snackis.Data;
using Snackis.Models;
using System.Security.Claims;


namespace Snackis.Pages
{
    public class MessagesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MessagesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PrivateMessage> PrivateMessages { get; set; }

        public async Task OnGetAsync()
        {
            var receiverId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            PrivateMessages = await _context.PrivateMessages.Where(pm => pm.ReceiverId == receiverId).ToListAsync();
        }
    }
}
