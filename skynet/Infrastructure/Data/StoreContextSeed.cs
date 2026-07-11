using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data;

public static class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productData = File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json").Result;
            var products = JsonSerializer.Deserialize<List<Product>>(productData);
            if (products != null)
            {
                foreach (var product in products)
                {
                    context.Products.Add(product);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}