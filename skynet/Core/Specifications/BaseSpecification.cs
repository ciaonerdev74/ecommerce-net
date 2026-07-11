using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria = null) : ISpecification<T>
{

    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct { get; protected set; } = false;

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpression)
    {
        OrderByDescending = orderByExpression;
    }

    protected void AddDistinct()
    {
        IsDistinct = true;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria = null)
: ISpecification<T, TResult>
{
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public Expression<Func<T, TResult>> Select { get; private set; } = null!;

    public bool IsDistinct { get; private set; }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpression)
    {
        OrderByDescending = orderByExpression;
    }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}


