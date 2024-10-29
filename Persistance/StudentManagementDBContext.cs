using Microsoft.EntityFrameworkCore;
using Studentmanagement.Models;

namespace Studentmanagement.Persistance
{
    public class StudentManagementDBContext : DbContext
    {

        public StudentManagementDBContext(DbContextOptions<StudentManagementDBContext> options) : base(options)
        {
        }

        // Define DbSets for your entities
        public DbSet<Course> Course { get; set; }
        public DbSet<User> User { get; set; }
        // Optionally, override OnModelCreating to configure entities
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add custom configurations here if needed
        }
    }
}
