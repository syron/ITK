using Microsoft.EntityFrameworkCore;
using KIT.Models;

namespace KIT.Services;

public class AppDbContext : DbContext
{
    public DbSet<Map>? Maps { get; set; }
    public DbSet<Schema>? Schemas { get; set; }
    public DbSet<SchemaExampleData>? SchemaExampleDatas { get; set; }
    public DbSet<HistoryEntry>? HistoryEntries { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Schema>()
                    .HasMany(e => e.InMaps)
                    .WithOne(e => e.InSchema)
                    .OnDelete(DeleteBehavior.NoAction);
                
        modelBuilder.Entity<Schema>()
                    .HasMany(e => e.OutMaps)
                    .WithOne(e => e.OutSchema)
                    .OnDelete(DeleteBehavior.NoAction);
    }
}  