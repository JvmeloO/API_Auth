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

        public virtual DbSet<EmailSent> EmailSents { get; set; } = null!;
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; } = null!;
        public virtual DbSet<EmailType> EmailTypes { get; set; } = null!;
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
            modelBuilder.Entity<EmailSent>(entity =>
            {
                entity.Property(e => e.RecipientEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.SenderEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailTemplate)
                    .WithMany(p => p.EmailSents)
                    .HasForeignKey(d => d.EmailTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailSents_EmailTemplateId");
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.HasIndex(e => e.TemplateName, "UQ_EmailTemplates")
                    .IsUnique();

                entity.Property(e => e.Content).IsUnicode(false);

                entity.Property(e => e.EmailSubject)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TemplateName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailType)
                    .WithMany(p => p.EmailTemplates)
                    .HasForeignKey(d => d.EmailTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmailTemplates_EmailTypeId");
            });

            modelBuilder.Entity<EmailType>(entity =>
            {
                entity.HasIndex(e => e.TypeName, "UQ_EmailTypes")
                    .IsUnique();

                entity.Property(e => e.TypeName)
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
