using Microsoft.EntityFrameworkCore;

namespace ProductService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductServiceDbContext>(options =>
                options.UseMySQL(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseRouting();

            // Маршрут для получения всех продуктов
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/api/products/isAlive", () => "Welcome to Product Service!");

                endpoints.MapGet("/api/products", async (ProductServiceDbContext db) =>
                    await db.Products.ToListAsync());

                // Маршрут для получения продукта по ID
                endpoints.MapGet("/api/products/{id:int}", async (int id, ProductServiceDbContext db) =>
                    await db.Products.FindAsync(id) is Product product
                        ? Results.Ok(product)
                        : Results.NotFound());

                // Маршрут для создания нового продукта
                endpoints.MapPost("/api/products", async (Product product, ProductServiceDbContext db) =>
                {
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return Results.Created($"/api/products/{product.Id}", product);
                });

                // Маршрут для обновления продукта
                endpoints.MapPut("/api/products/{id:int}", async (int id, Product updatedProduct, ProductServiceDbContext db) =>
                {
                    var product = await db.Products.FindAsync(id);
                    if (product == null) return Results.NotFound();

                    product.Name = updatedProduct.Name;
                    product.Price = updatedProduct.Price;

                    await db.SaveChangesAsync();
                    return Results.NoContent();
                });

                // Маршрут для удаления продукта
                endpoints.MapDelete("/api/products/{id:int}", async (int id, ProductServiceDbContext db) =>
                {
                    var product = await db.Products.FindAsync(id);
                    if (product == null) return Results.NotFound();

                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                });
            });
        }
    }
}