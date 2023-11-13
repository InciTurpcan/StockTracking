using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Context;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> opt) : base(opt)
    {
        
    }

    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }

}
