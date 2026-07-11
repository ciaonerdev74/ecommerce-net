using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brands, string? types, string? sort)
    {
        var products = await repo.GetProductsAsync(brands, types, sort);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }


    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
        if (await repo.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        else
        {
            return BadRequest("Failed to create product");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (!ProductExists(id))
        {
            return BadRequest("Invalid Product ID");
        }

        var existingProduct = await repo.GetProductByIdAsync(id);

        existingProduct!.Name = product.Name;
        existingProduct!.Description = product.Description;
        existingProduct!.Price = product.Price;
        existingProduct!.QuantityInStock = product.QuantityInStock;
        existingProduct!.PictureUrl = product.PictureUrl;
        existingProduct!.Type = product.Type;
        existingProduct!.Brand = product.Brand;

        await repo.SaveChangesAsync();
        return Ok(existingProduct);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        if (!ProductExists(id))
        {
            return NotFound();
        }
        var existingProduct = await repo.GetProductByIdAsync(id);
        repo.Delete(existingProduct!);
        await repo.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var brands = await repo.GetBrandAsync();
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var types = await repo.GetTypeAsync();
        return Ok(types);
    }

    private bool ProductExists(int id)
    {
        var product = repo.ProductExists(id);
        return product;
    }
}
