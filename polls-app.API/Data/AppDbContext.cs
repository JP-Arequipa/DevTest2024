using Microsoft.EntityFrameworkCore;
using polls_app.API.Models;

namespace polls_app.API.Data;

public class AppDbContext : DbContext
{
    private DbSet<Poll> Polls;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}