using Microsoft.EntityFrameworkCore;

namespace projet__Chokri_Forum.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

      public DbSet<Users> Users { get; set; } 
      public DbSet<Forums> Forums { get; set; }
      public DbSet<Themes> Themes { get; set; }
      public DbSet<Posts> Posts { get; set; }
      public DbSet<Messages> Messages { get; set; }

    }
}
