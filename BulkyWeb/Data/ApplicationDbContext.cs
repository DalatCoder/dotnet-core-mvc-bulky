using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Category>  Categories { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}