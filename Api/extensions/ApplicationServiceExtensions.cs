using System;
using FluentValidation;
using MediatR;
using CleanShop.Application.Abstractions;
using CleanShop.Infrastructure.Persistence.Repositories;
using CleanShop.Api.Mappings;
using Microsoft.VisualBasic;
using System.Threading.RateLimiting;
namespace CleanShop.Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            HashSet<String> allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "https://app.ejemplo.com",
                "https://admin.ejemplo.com"
            };

            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()   //WithOrigins("https://dominio.com")
                .AllowAnyMethod()          //WithMethods("GET","POST")
                .AllowAnyHeader());        //WithHeaders("accept","content-type")

            options.AddPolicy("CorsPolicyUrl", builder =>
            builder.WithOrigins("https://localhost:4200", "https://localhost:5500")
            .AllowAnyMethod()
            .AllowAnyHeader());

            options.AddPolicy("Dinamica", builder =>
            builder.SetIsOriginAllowed(origin => allowed.Contains(origin))
            .WithMethods("GET", "POST")
            .WithHeaders("Content-Type", "Authorization"));
        });
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddAutoMapper(typeof(ProductProfile).Assembly);
    }

    public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.OnRejected = async (context, token) =>
            {
                var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "desconocida";
                context.HttpContext.Response.StatusCode = 429;
                context.HttpContext.Response.ContentType = "application/json";
                var mensaje = $"{{\"message\": \"demasiadas peticiones desde la ip {ip}. intenta mas tarde.\"}}";
                await context.HttpContext.Response.WriteAsync(mensaje, token);
            };

            options.AddPolicy("iplimiter", HttpContext =>
            {
                var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown0";
                return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromSeconds(10),
                    QueueLimit = 0,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                });
            });
        });

        return services;
    }
}
