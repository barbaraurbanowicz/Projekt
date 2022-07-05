using Microsoft.EntityFrameworkCore;

namespace SpendingTrackerAPI.Database;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    
}