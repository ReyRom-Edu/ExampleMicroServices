using Microsoft.EntityFrameworkCore;

namespace ProductService
{
    public class ProductServiceDbContext:DbContext
    {

        public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
    }
}