using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureGlobalFilters<Employee>(modelBuilder);
            
        }
        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder) where TEntity : class
        {
            if (ShouldFilterEntity<TEntity>())
            {
                var filterEx=CreateFilterExpression<TEntity>();
                if(filterEx!=null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterEx);
                }

            }
        }
        protected virtual bool ShouldFilterEntity<TEntity>() where TEntity : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }
            return false;
        }
        protected virtual Expression<Func<TEntity,bool>> CreateFilterExpression<TEntity>() where TEntity : class
        {
            Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted;
            return softDeleteFilter;
            
        }
    }
}
