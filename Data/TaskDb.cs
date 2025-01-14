using Microsoft.EntityFrameworkCore;
using TaskBackendAPI.Models;
namespace TaskBackendAPI.Data
{
    public class TaskDb : DbContext
    {
        public TaskDb()
        {
            
        }
        public TaskDb(DbContextOptions<TaskDb> options): base(options)
        {

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Models.Task> Tasks => Set<Models.Task>();

    }
}
