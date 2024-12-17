using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Infrastructure.Adapters.Persistence;
using UniqueDraw.Infrastructure.Adapters.Persistence.EFContext;
using UniqueDraw.Infrastructure.Adapters.Persistence.Repositories;

namespace UniqueDraw.Infrastructure.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPesistenceServices(this IServiceCollection services, IConfiguration configuration)
    {

        var stringConnection = configuration.GetConnectionString("DefaultConnection");

        services.AddTransient<IDbConnection>((sp) =>
        new SqlConnection(stringConnection));

        services.AddDbContext<UniqueDrawDbContext>(options =>
            options.UseSqlServer(stringConnection));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient(typeof(IRepository<>), typeof(EFCoreRepository<>));

        services.AddTransient<IRepository<Product>, DapperRepository<Product>>();

        return services;
    }
}