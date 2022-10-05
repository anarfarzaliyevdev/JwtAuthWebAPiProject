using JwtAuthWebAPiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthWebAPiProject.DbContexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
: base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Permisson> Permissons { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
