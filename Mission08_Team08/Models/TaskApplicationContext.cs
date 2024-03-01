using Microsoft.EntityFrameworkCore;

namespace Mission08_Team08.Models
{
    public class TaskApplicationContext : DbContext
    {
        public TaskApplicationContext(DbContextOptions<TaskApplicationContext> options) : base (options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
    }
}
