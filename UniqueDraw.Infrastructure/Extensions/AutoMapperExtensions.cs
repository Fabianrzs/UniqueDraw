using Microsoft.Extensions.DependencyInjection;

namespace UniqueDraw.Infrastructure.Extensions;

public static class AutoMapperExtensions
{
    public static IServiceCollection AddMapperServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppAssembly.InfrastructureAssembly);
        return services;
    }
}