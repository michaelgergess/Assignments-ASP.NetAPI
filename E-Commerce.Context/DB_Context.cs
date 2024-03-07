using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Context
{
    public class DB_Context:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> userRole { get; set; }


        public DB_Context(DbContextOptions option) : base(option)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                           .HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}
