﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UniqueDraw.Domain.Ports;
using UniqueDraw.Domain.Ports.Security;
using UniqueDraw.Infrastructure.Adapters.Security;
using System.Text;

namespace UniqueDraw.Infrastructure.Extensions;
public static class SecurityExtensions
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettingKey = configuration["JwtSettings:Key"]!;
        var encriptionKey = configuration["Encryption:Key"]!;

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettingKey!))
            };
        });

        services.AddScoped<ITokenService>(provider =>
            new TokenService(
                jwtSettingKey,
                configuration["JwtSettings:Issuer"]!,
                configuration["JwtSettings:Audience"]!
            ));

        services.AddScoped<IEncryptionService>(provider =>
            new EncryptionService(encriptionKey));

        return services;
    }
}

