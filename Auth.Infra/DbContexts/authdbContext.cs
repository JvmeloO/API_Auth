using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Auth.Domain.Entities;

namespace Auth.Infra.DbContexts
{
    public partial class authdbContext : DbContext
    {
        public authdbContext()
        {
        }

        public authdbContext(DbContextOptions<authdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmailsSent> EmailsSents { get; set; } = null!;
        public virtual DbSet<EmailsType> EmailsTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:authdb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailsSent>(entity =>
            {
                entity.HasKey(e => e.EmailSentId);

                entity.Property(e => e.Content)
                    .HasMaxLength(7000)
                    .IsUnicode(false);

                entity.Property(e => e.RecipientEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.SenderEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubjectEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailType)
                    .WithMany(p => p.EmailsSents)
                    .HasForeignKey(d => d.EmailTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailsSents_EmailTypeId");
            });

            modelBuilder.Entity<EmailsType>(entity =>
            {
                entity.HasKey(e => e.EmailTypeId);

                entity.HasIndex(e => e.EmailTypeName, "UQ_EmailsTypes")
                    .IsUnique();

                entity.Property(e => e.EmailTypeName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleName, "UQ_Roles")
                    .IsUnique();

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => new { e.Username, e.Email }, "UQ_Users")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRoles_RoleId"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRoles_UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("UserRoles");

                            j.HasIndex(new[] { "UserId", "RoleId" }, "UQ_UserRoles").IsUnique();
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
