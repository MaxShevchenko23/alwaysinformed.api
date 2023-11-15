using System;
using System.Collections.Generic;
using alwaysinformed_dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace alwaysinformed_dal.Data;

public partial class AidbContext : DbContext
{
    public AidbContext()
    {
    }

    public AidbContext(DbContextOptions<AidbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleSandbox> ArticleSandboxes { get; set; }

    public virtual DbSet<ArticleSandboxStatus> ArticleSandboxStatuses { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KKLFTJP;Database=aidb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.Property(e => e.Content).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Image).HasDefaultValueSql("(N'')");
            entity.Property(e => e.PublicationDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.ShortDescription).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Title).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Url)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("URL");

            entity.HasOne(d => d.ArticleSandbox).WithMany(p => p.Articles)
                .HasForeignKey(d => d.ArticleSandboxId)
                .HasConstraintName("FK_Articles_ArticleSandbox");

            entity.HasOne(d => d.Author).WithMany(p => p.Articles)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Category).WithMany(p => p.Articles)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ArticleSandbox>(entity =>
        {
            entity.HasKey(e => e.SandboxId);

            entity.ToTable("ArticleSandbox");

            entity.Property(e => e.AdminEmail).HasMaxLength(50);
            entity.Property(e => e.ArticleAdminComment).HasMaxLength(50);
            entity.Property(e => e.Content).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Image).HasDefaultValueSql("(N'')");
            entity.Property(e => e.PublicationDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.ShortDescription).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Title).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Url)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("URL");

            entity.HasOne(d => d.ArticleStatusNavigation).WithMany(p => p.ArticleSandboxes)
                .HasForeignKey(d => d.ArticleStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleSandbox_ArticleSandboxStatuses");

            entity.HasOne(d => d.Author).WithMany(p => p.ArticleSandboxes)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ArticleSandbox_Authors");

            entity.HasOne(d => d.Category).WithMany(p => p.ArticleSandboxes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ArticleSandbox_Categories");
        });

        modelBuilder.Entity<ArticleSandboxStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Authors)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Authors_Users");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.Article).WithMany(p => p.Comments).HasForeignKey(d => d.ArticleId);
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasOne(d => d.Article).WithMany(p => p.Favorites).HasForeignKey(d => d.ArticleId);

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserRoles");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.Property(e => e.UserRoleName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
