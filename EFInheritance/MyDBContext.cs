using EFInheritance.Models.TPC;
using EFInheritance.Models.TPH;
using EFInheritance.Models.TPT;
using Microsoft.EntityFrameworkCore;

namespace EFInheritance;

public class MyDBContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPH
        modelBuilder.Entity<Animal2>().UseTphMappingStrategy();
        modelBuilder.Entity<Dog2>();
        modelBuilder.Entity<Cat2>();

        // TPC
        modelBuilder.Entity<Dog1>().UseTpcMappingStrategy();
        modelBuilder.Entity<Cat1>().UseTpcMappingStrategy();

        // TPT
        modelBuilder.Entity<Animal3>().UseTptMappingStrategy();
        modelBuilder.Entity<Dog3>();
        modelBuilder.Entity<Cat3>();

        base.OnModelCreating(modelBuilder);
    }
}

/*

Mapping Strategy

TPH (Table Per Hierachy) : Animal
Pros: no need to join, Simple
Cons: Many nulls, Effect of changes

TPC (Table Per Concrete class) [DEFAULT] : Dogs, Cats
Pros: Seperation, No nulls
Cons: Query of all animal

TPT (Table Per Type) : Animal, Dogs, Cats
Pros: Easy to extend
Cons: Complex, Slower

*/