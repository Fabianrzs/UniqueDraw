using Dapper;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using UniqueDraw.Domain.Entities.Base;
using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Infrastructure.Helpers;

namespace UniqueDraw.Infrastructure.Adapters.Persistence;
public class DapperRepository<T>(IDbConnection connection) : IRepository<T> where T : EntityBase
{
    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        var tableName = typeof(T).Name;
        var sql = $"SELECT * FROM {tableName} WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var tableName = typeof(T).Name;
        var sql = $"SELECT * FROM {tableName}";
        return await connection.QueryAsync<T>(sql);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), "El predicado no puede ser nulo.");
        }

        var tableName = typeof(T).Name;

        string whereClause = ExpressionToSqlTranslator.Translate(predicate);

        var sql = $"SELECT * FROM {tableName} WHERE {whereClause}";

        return await connection.QueryAsync<T>(sql);
    }

    public async Task AddAsync(T entity)
    {
        var spName = $"sp_add{typeof(T).Name}";
        var parameters = GetParameters(entity);
        await connection.ExecuteAsync(spName, parameters, 
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateAsync(T entity)
    {
        var spName = $"sp_update{typeof(T).Name}";
        var parameters = GetParameters(entity);
        await connection.ExecuteAsync(spName, parameters,
            commandType: CommandType.StoredProcedure);
    }


    public async Task DeleteAsync(T entity)
    {
        var spName = $"sp_delete{typeof(T).Name}";
        await connection.ExecuteAsync(spName, 
            new { entity.Id }, commandType: CommandType.StoredProcedure); 
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate), "El predicado no puede ser nulo.");
        }

        var tableName = typeof(T).Name;

        string whereClause = ExpressionToSqlTranslator.Translate(predicate);

        var sql = $"SELECT 1 FROM {tableName} WHERE {whereClause}";

        var result = await connection.QuerySingleOrDefaultAsync<int>(sql);

        return result > 0;
    }

    private static DynamicParameters GetParameters(T entity)
    {
        var parameters = new DynamicParameters();
        foreach (var property in typeof(T).GetProperties())
        {
            if (IsSupportedSqlType(property.PropertyType))
            {
                var value = property.GetValue(entity);
                if (value != null && (!IsDefaultValue(value)
                    || value.GetType() == typeof(bool)))
                {
                    parameters.Add($"@{property.Name}", value);
                }
            }
        }
        return parameters;
    }

    private static string GetSqlQueryFromExpression(Expression<Func<T, bool>> filter)
    {
        var queryBody = filter.Body.ToString();
        var paramName = filter.Parameters[0].Name;
        var query = new StringBuilder(queryBody);
        query.Replace(paramName + ".", string.Empty);
        query.Replace("AndAlso", "AND");
        query.Replace("OrElse", "OR");
        return $"SELECT * FROM {typeof(T).Name} WHERE {query}";
    }
    private static bool IsSupportedSqlType(Type type) =>
     type.IsPrimitive || type.IsEnum || type == typeof(string) ||
     type == typeof(decimal) || type == typeof(DateTime) ||
     type == typeof(Guid) || type == typeof(TimeSpan) ||
     type == typeof(byte[]);

    private static bool IsDefaultValue(object value) =>
        value == null ||
        (value is string str && string.IsNullOrEmpty(str)) ||
        (value.GetType().IsValueType &&
        value.Equals(Activator.CreateInstance(value.GetType())));
}