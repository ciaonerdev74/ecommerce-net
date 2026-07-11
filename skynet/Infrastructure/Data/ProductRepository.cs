using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brands, string? types, string? sort)
    {
        var query = context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(brands))
        {
            query = query.Where(x => x.Brand.ToLower() == brands.ToLower());
        }

        if (!string.IsNullOrEmpty(types))
        {
            query = query.Where(x => x.Type == types);
        }

        if (!string.IsNullOrEmpty(sort))
        {
            query = sort switch
            {
                "priceAsc" => query.OrderBy(x => x.Price),
                "priceDesc" => query.OrderByDescending(x => x.Price),
                "nameAsc" => query.OrderBy(x => x.Name),
                "nameDesc" => query.OrderByDescending(x => x.Name),
                _ => query
            };
        }

        return await query.ToListAsync();
    }



    public void Add(Product product)
    {
        context.Products.Add(product);
    }

    public void Update(Product product)
    {
        context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        context.Products.Remove(product);
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IReadOnlyList<string>> GetBrandAsync()
    {
        return await context.Products.Select(x => x.Brand).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypeAsync()
    {
        return await context.Products.Select(x => x.Type).Distinct().ToListAsync();
    }
}
