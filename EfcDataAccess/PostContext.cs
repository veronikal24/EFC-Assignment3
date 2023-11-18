using Domain_A1.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class PostContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        SQLitePCL.Batteries.Init(); // Or use SQLitePCL.raw.SetProvider() if needed
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Post.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);            
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId); // Assuming UserId is the correct foreign key property

        // Other configurations...

        base.OnModelCreating(modelBuilder);
    }
}