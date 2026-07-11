using Core.Entities;

namespace Core.Specifications;

public class TypeListSpecification : BaseSpecification<Product, string>
{
    public TypeListSpecification() : base(null)
    {
        AddSelect(x => x.Type!);
        ApplyDistinct();

    }
}
