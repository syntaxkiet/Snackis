using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Snackis.Models;
using System.Reflection.Emit;

namespace Snackis.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { 
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Models.Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";
            var regularUser = new IdentityRole("regularUser");
            regularUser.NormalizedName = "regularUser";
            builder.Entity<IdentityRole>().HasData(admin, regularUser);

            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Models.Thread>().ToTable("Threads");
            builder.Entity<Post>().ToTable("Posts");

            builder.Entity<Category>()
                .HasMany(c => c.Threads)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId);

            builder.Entity<Models.Thread>()
                .HasMany(t => t.Posts)
                .WithOne(p => p.Thread)
                .HasForeignKey(p => p.ThreadId);
            
        }
    }
}
