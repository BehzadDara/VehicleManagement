using CarBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace CarBlog;

public class CarBlogDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(c =>
        {
            c.HasKey(x => x.Id);
            c.Property(x => x.Id).ValueGeneratedNever();
            c.Property(x => x.Title).HasMaxLength(100);
            c.Property(x => x.Gearbox).HasMaxLength(10);
        });

        base.OnModelCreating(modelBuilder);
    }
}
