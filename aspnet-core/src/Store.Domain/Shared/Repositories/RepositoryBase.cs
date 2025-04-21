using System.Linq.Expressions;
using Store.Shared.Entities;
using Store.Shared.Validations;

namespace Store.Shared.Repositories;

public abstract class RepositoryBase<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
    {
        ArgumentValidator.NotNull(id!);

        var lambdaParam = Expression.Parameter(typeof(TEntity));
        var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");
        var rightExpression = Expression.Constant(id, leftExpression.Type);
        var lambdaBody = Expression.Equal(leftExpression, rightExpression);

        return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
    }
}
