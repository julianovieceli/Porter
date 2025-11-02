using System.Linq.Expressions;

namespace Porter.Common.Utils.ExtensionMethods;

// Helper to combine Expression<Func<T, bool>> predicates
public static class ExpressionExtensions
{
    // Combines two expressions with an AND logical operator (&&)
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        // 1. Create a parameter for the new combined expression (e.g., 'x')
        var parameter = Expression.Parameter(typeof(T), "x");

        // 2. Map the parameter of the second expression to the first one
        var invokedSecond = Expression.Invoke(second, parameter);

        // 3. Combine the bodies with AND
        var combinedBody = Expression.AndAlso(first.Body, invokedSecond);

        // 4. Create a new lambda with the combined body and the new parameter
        return Expression.Lambda<Func<T, bool>>(combinedBody, parameter);
    }

    // You would need a custom ExpressionVisitor for IQueryable translation 
    // to replace the parameter cleanly (more complex).
    // A simpler approach for IQueryable is to use an "ParameterReplacer":
    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;
        public ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }
    }

    // Simpler AND for IQueryable that uses the ParameterReplacer (often found online)
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var parameter = left.Parameters[0];
        var rightBody = new ParameterReplacer(parameter).Visit(right.Body);
        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(left.Body, rightBody), parameter);
    }
}