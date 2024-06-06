using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Snackis.Models
{
    public class Thread
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Category Category { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
