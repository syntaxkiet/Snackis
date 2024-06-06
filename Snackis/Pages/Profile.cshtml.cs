using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Snackis.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public User? appUser;

        public ProfileModel(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            appUser = userManager.GetUserAsync(User).Result;
        }

        public async Task<IActionResult> OnPostAsync(IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                var user = await userManager.GetUserAsync(User);

                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var imagePath = Path.Combine(uploads, profileImage.FileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(fileStream);
                }

                user.ProfileImage = profileImage.FileName;
                await userManager.UpdateAsync(user);
            }

            return RedirectToPage();
        }
    }
}