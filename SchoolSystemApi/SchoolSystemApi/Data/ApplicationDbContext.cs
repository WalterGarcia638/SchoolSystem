using Microsoft.EntityFrameworkCore;
using SchoolSystemApi.Model;

namespace SchoolSystemApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set;}
     

    }
}