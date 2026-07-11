namespace Core.Specifications;

using Core.Entities;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(string? brand, string? type, string? sort) : base(x =>
        (string.IsNullOrWhiteSpace(brand) || x.Brand.ToLower() == brand.ToLower()) &&
        (string.IsNullOrWhiteSpace(type) || x.Type.ToLower() == type.ToLower()))
    {
        switch (sort?.ToLower())
        {
            case "price":
            case "priceasc":
                AddOrderBy(x => x.Price);
                break;
            case "pricedesc":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}