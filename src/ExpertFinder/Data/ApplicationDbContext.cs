using ExpertFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Article> Articles => Set<Article>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Like> Likes => Set<Like>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasKey(x => x.Id);

        modelBuilder.Entity<User>()
            .HasMany<Article>()
            .WithOne(x => x.Author)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .Property<byte[]>("Version")
            .IsRowVersion();

        modelBuilder.Entity<Article>()
            .Property<byte[]>("Version")
            .IsRowVersion();

        modelBuilder.Entity<Like>().HasKey(x => new { x.UserId, x.ArticleId });
        modelBuilder.Entity<Like>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Like>().HasOne(x => x.Article).WithMany().HasForeignKey(x => x.ArticleId).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>().HasData(
            new User { FullName = "Willem Meints", Id = Guid.Parse("aa828b23-96fa-4cb0-8d22-5e89a10ba3cd") },
            new User { FullName = "Joop Snijder", Id = Guid.Parse("8ba4ba14-bbd0-4f7c-a85a-90725f2bd93f") },
            new User { FullName = "Emiel Stoelinga", Id = Guid.Parse("bef98bd4-795c-46b7-9295-427de33cb6a6") },
            new User { FullName = "Rowan Terinathe", Id = Guid.Parse("589ee3c2-4e59-48fb-a5ca-24dd5960d30a") },
            new User { FullName = "Lucia Conde Moreno", Id = Guid.Parse("20b18172-f06b-454b-bd48-3c5573fc4a1c") }
        );
    }
}