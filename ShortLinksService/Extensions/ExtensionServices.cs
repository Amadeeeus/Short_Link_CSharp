using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShortLinksService.Commands.Create;
using ShortLinksService.Controllers;
using ShortLinksService.Repositories;

namespace ShortLinksService.Extensions;

public static class ExtensionServices
{
    public static void AddStackExchangeRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration =configuration.GetSection("Redis:Configuration").Value;
            options.InstanceName = configuration.GetSection("Redis:InstanceName").Value;
        });
    }

    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton(new MongoClient("mongodb://admin:secret@localhost:27017/links"));
        services.AddScoped<IShortLinkRepository, ShortLinkRepository>();
        services.AddScoped<PasswordHasher>();
    }
}