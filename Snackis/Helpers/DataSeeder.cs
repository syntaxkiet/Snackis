using Snackis.Data;
using Snackis.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Snackis.Helpers
{
    public class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var category = new Category
                {
                    Name = "General Discussion",
                    Description = "Talk about anything here.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Threads = new List<Models.Thread>
                {
                    new Models.Thread
                    {
                        Title = "Welcome to the forum",
                        Author = new string("0cc023ca-149f-4b29-9ed2-46c49cfe3db8"),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Posts = new List<Post>
                        {
                            new Post
                            {
                                Author = new string("0cc023ca-149f-4b29-9ed2-46c49cfe3db8"),
                                Text = "Welcome everyone! Feel free to start discussions.",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow
                            }
                        }
                    }
                }
                };

                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
    }
}
