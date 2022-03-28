using API_Auth.DTO;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }

        public DbSet<RoleDTO> Roles { get; set; }

        public DbSet<UserRoleDTO> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDTO>().HasKey(u => u.User_Id);

            modelBuilder.Entity<RoleDTO>().HasKey(r => r.Role_Id);

            modelBuilder.Entity<UserRoleDTO>().HasKey(ur =>
            new
            {
                ur.User_Id, ur.Role_Id
            });

            // Relationships Many to Many
            modelBuilder.Entity<UserRoleDTO>()
                .HasOne(u => u.User)
                .WithMany(ur => ur.UserRoles)
                .HasForeignKey(u => u.User_Id);

            modelBuilder.Entity<UserRoleDTO>()
               .HasOne(u => u.Role)
               .WithMany(ur => ur.UserRoles)
               .HasForeignKey(u => u.Role_Id);
        }
    }
}
