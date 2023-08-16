namespace WebEng.Properties.Models;

using Microsoft.EntityFrameworkCore;

// A "DbContext" implementation represents a connection to the database through
// Entity Framework. You typically define two things here:
// 1. All entities that you want to include in your database - those will
//    be converted into tables and you add them by adding a property of type
//    DbSet<T> to this class.
// 2. Any additional configuration you did not put in attributes in the entity
//    classes themselves. In this case, we configure explicitly the many-to-many
//    relation between movies and actors, and movies and languages.
public class DatabaseContext : DbContext
{
    public DbSet<Property> Properties { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Statistics> Statistics { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>()
            .HasOne(m => m.User)
            .WithMany(a => a.Properties);
        modelBuilder.Entity<Property>()
            .HasOne<Statistics>()
            .WithMany(a => a.Properties)
            .HasForeignKey(p => p.City);
    }
}