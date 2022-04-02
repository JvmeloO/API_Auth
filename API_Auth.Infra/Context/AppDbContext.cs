using API_Auth.Domain.Configurations;
using API_Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Infra.Context
{
    public class AppDbContext : DbContext
    {
        private readonly BaseConfigurations _config;

        public AppDbContext(BaseConfigurations config) 
        {
            _config = config;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);

            modelBuilder.Entity<UserRole>().HasKey(ur =>
            new
            {
                ur.UserId, ur.RoleId
            });

            // Relationships Many to Many
            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.User)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserRole>()
               .HasOne(u => u.Role)
               .WithMany(ur => ur.UserRoles)
               .HasForeignKey(u => u.RoleId);
        }
    }
}
