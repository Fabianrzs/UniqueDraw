using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Infrastructure.Adapters.Persistence.EFContext;
using UniqueDraw.Infrastructure.Adapters.Persistence.Repositories;

namespace UniqueDraw.Infrastructure.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPesistenceServices(this IServiceCollection svc, IConfiguration config)
    {
        var user = config["UniqueDraw.DbConections-UserId"];
        var password = config["UniqueDraw.DbConections-Password"];
        var server = config["UniqueDraw.DbConections-Server"];
        var database = config["UniqueDraw.DbConections-Database"];

        var stringConnection = config.GetConnectionString("DefaultConnection");
        svc.AddDbContext<UniqueDrawDbContext>(options =>
            options.UseSqlServer(stringConnection));

        svc.AddTransient<IDbConnection>((sp) => new SqlConnection(config.GetConnectionString("DefaultConnection")));

        svc.AddScoped<IUnitOfWork, UnitOfWork>();

        /*svc.AddTransient(typeof(IRepository<>), typeof(Repository<>));*/
        return svc;
    }
}