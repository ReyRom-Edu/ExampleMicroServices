using Microsoft.EntityFrameworkCore;

namespace ProductService
{
    public class ProductServiceDbContext:DbContext
    {

        public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
    }
}