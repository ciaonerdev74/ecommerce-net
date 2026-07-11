using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        var spec = new ProductSpecification(brand, type, sort);
        var products = await repo.ListAsync(spec);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
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
        if (await repo.SaveAllAsync())
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
        var existingProduct = await repo.GetByIdAsync(id);

        if (existingProduct == null)
        {
            return BadRequest("Invalid Product ID");
        }

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.QuantityInStock = product.QuantityInStock;
        existingProduct.PictureUrl = product.PictureUrl;
        existingProduct.Type = product.Type;
        existingProduct.Brand = product.Brand;

        await repo.SaveAllAsync();
        return Ok(existingProduct);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var existingProduct = await repo.GetByIdAsync(id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        repo.Delete(existingProduct);
        await repo.SaveAllAsync();
        return Ok();
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        var brands = await repo.ListAsync(spec);
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        var types = await repo.ListAsync(spec);
        return Ok(types);
    }
}
