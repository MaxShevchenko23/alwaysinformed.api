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

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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

            entity.HasOne(d => d.Author).WithMany(p => p.Articles)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Category).WithMany(p => p.Articles)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ArticleSandbox>(entity =>
        {
            entity.HasKey(e => e.ArticleId);

            entity.ToTable("ArticleSandbox");

            entity.Property(e => e.Content).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Image).HasDefaultValueSql("(N'')");
            entity.Property(e => e.PublicationDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.ShortDescription).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Title).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Url)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("URL");

            entity.HasOne(d => d.Author).WithMany(p => p.ArticleSandboxes)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ArticleSandbox_Authors");

            entity.HasOne(d => d.Category).WithMany(p => p.ArticleSandboxes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ArticleSandbox_Categories");
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
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
