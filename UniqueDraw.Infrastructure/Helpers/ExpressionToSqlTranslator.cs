using System.Linq.Expressions;

namespace UniqueDraw.Infrastructure.Helpers;
public static class ExpressionToSqlTranslator
{
    public static string Translate<T>(Expression<Func<T, bool>> expression)
    {
        if (expression.Body is BinaryExpression binaryExpression)
        {
            return TranslateBinaryExpression(binaryExpression);
        }

        throw new NotSupportedException("Solo se soportan expresiones binarias.");
    }

    private static string TranslateBinaryExpression(BinaryExpression binaryExpression)
    {
        var left = TranslateExpression(binaryExpression.Left);
        var right = TranslateExpression(binaryExpression.Right);
        var @operator = TranslateOperator(binaryExpression.NodeType);

        return $"{left} {@operator} {right}";
    }

    private static string TranslateExpression(Expression expression)
    {
        return expression switch
        {
            MemberExpression member => member.Member.Name,
            ConstantExpression constant => $"'{constant.Value}'",
            _ => throw new NotSupportedException($"Tipo de expresión no soportada: {expression.NodeType}")
        };
    }

    private static string TranslateOperator(ExpressionType expressionType)
    {
        return expressionType switch
        {
            ExpressionType.Equal => "=",
            ExpressionType.NotEqual => "<>",
            ExpressionType.GreaterThan => ">",
            ExpressionType.GreaterThanOrEqual => ">=",
            ExpressionType.LessThan => "<",
            ExpressionType.LessThanOrEqual => "<=",
            _ => throw new NotSupportedException($"Operador no soportado: {expressionType}")
        };
    }
}
