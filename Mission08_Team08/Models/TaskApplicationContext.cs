using Microsoft.EntityFrameworkCore;

namespace Mission08_Team08.Models
{
    public class TaskApplicationContext : DbContext
    {
        public TaskApplicationContext(DbContextOptions<TaskApplicationContext> options) : base (options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                new Category { CategoryId = 1, CategoryName = "Home"},
                new Category { CategoryId = 2, CategoryName = "School" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Church" }
                );
        }
    }
}
