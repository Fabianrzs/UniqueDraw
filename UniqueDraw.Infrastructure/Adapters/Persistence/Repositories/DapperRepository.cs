using Dapper;
using System.Data;
using System.Linq.Expressions;
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
        var tableName = typeof(T).Name;
        var columns = string.Join(", ", entity.GetType().GetProperties().Select(p => p.Name));
        var values = string.Join(", ", entity.GetType().GetProperties().Select(p => $"@{p.Name}"));

        var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
        await connection.ExecuteAsync(sql, entity);
    }

    public async Task UpdateAsync(T entity)
    {
        var tableName = typeof(T).Name;
        var setClause = string.Join(", ", entity.GetType().GetProperties().Select(p => $"{p.Name} = @{p.Name}"));

        var sql = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";
        await connection.ExecuteAsync(sql, entity);
    }

    public async Task DeleteAsync(T entity)
    {
        var tableName = typeof(T).Name;
        var sql = $"DELETE FROM {tableName} WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { entity.Id });
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
}