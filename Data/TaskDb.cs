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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Task>()
                .HasOne(t => t.userAssigned)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.userAssignedId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Models.Task>()
                .Property(t => t.status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v));

            modelBuilder.Entity<User>()
                .Property(u => u.role)
                .HasConversion(
                       v => v.ToString(),  
                       v => (Role)Enum.Parse(typeof(Role), v));  
            base.OnModelCreating(modelBuilder);

        }
    }
}
