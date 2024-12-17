using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UniqueDraw.Infrastructure.Extensions;

public static class ValidationExtension
{
    public static IServiceCollection AddValidatorServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
