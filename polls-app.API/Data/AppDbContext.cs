using Microsoft.EntityFrameworkCore;
using polls_app.API.Models;

namespace polls_app.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Poll> Polls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Poll>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}