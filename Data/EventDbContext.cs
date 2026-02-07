using EventEase.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Data;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events => Set<Event>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
            .HasIndex(e => e.EventDate)
            .HasDatabaseName("IX_Events_Date");

        modelBuilder.Entity<Event>()
            .HasIndex(e => e.Title)
            .HasDatabaseName("IX_Events_Title");

        modelBuilder.Entity<User>()
            .HasIndex(u => u.EmailEncrypted)
            .IsUnique();
    }
}
