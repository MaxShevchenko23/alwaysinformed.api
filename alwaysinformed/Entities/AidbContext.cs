using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace alwaysinformed.Entities;

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

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=DESKTOP-KKLFTJP;Database=aidb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasIndex(e => e.AuthorId, "IX_Articles_AuthorId");

            entity.HasIndex(e => e.CategoryId, "IX_Articles_CategoryId");

            entity.Property(e => e.Content).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Image).HasDefaultValueSql("(N'')");
            entity.Property(e => e.PublicationDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.ShortDescription).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Title).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Url)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("URL");

            entity.HasOne(d => d.Author).WithMany(p => p.Articles).HasForeignKey(d => d.AuthorId);

            entity.HasOne(d => d.Category).WithMany(p => p.Articles).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasIndex(e => e.ArticleId, "IX_Comments_ArticleId");

            entity.HasOne(d => d.Article).WithMany(p => p.Comments).HasForeignKey(d => d.ArticleId);
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasIndex(e => e.ArticleId, "IX_Favorites_ArticleId");

            entity.HasIndex(e => e.UserId, "IX_Favorites_UserId");

            entity.HasOne(d => d.Article).WithMany(p => p.Favorites).HasForeignKey(d => d.ArticleId);

            entity.HasOne(d => d.User).WithMany(p => p.Favorites).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
