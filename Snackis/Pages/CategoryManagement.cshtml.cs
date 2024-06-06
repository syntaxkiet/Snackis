using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis.Data;
using Snackis.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Snackis.Pages
{
    public class CategoryManagementModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Category> Categories { get; set; }

        [BindProperty]
        public Category NewCategory { get; set; }

        public CategoryManagementModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {

                NewCategory.CreatedAt = DateTime.Now;
                NewCategory.UpdatedAt = DateTime.Now;
                NewCategory.Threads = new List<Models.Thread>();
                _context.Categories.Add(NewCategory);
                await _context.SaveChangesAsync();
                Console.WriteLine("Category added successfully."); // Add this line for debugging
                return RedirectToPage("./CategoryManagement");

        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./CategoryManagement");
        }
    }
}