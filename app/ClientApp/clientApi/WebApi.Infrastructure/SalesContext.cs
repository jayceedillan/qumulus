using Microsoft.EntityFrameworkCore;
using WebApi.Qumulus.Domain;

namespace WebApi.Qumulus.Infrastructure
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}