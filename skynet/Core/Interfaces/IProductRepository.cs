using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brands, string? types, string? sort);
    Task<Product?> GetProductByIdAsync(int id);

    Task<IReadOnlyList<string>> GetBrandAsync();
    Task<IReadOnlyList<string>> GetTypeAsync();

    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}
