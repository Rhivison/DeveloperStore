using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class RedisModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379,password=ev@luAt10n,abortConnect=false";
        var configurationOptions = ConfigurationOptions.Parse(redisConnectionString);
        
        // Configurações específicas para o seu setup
        configurationOptions.AbortOnConnectFail = false;
        configurationOptions.ConnectRetry = 3;
        configurationOptions.ConnectTimeout = 10000; 
        configurationOptions.SyncTimeout = 5000;
        configurationOptions.Password = "ev@luAt10n"; 

        // Registra Redis como opcional
        builder.Services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
        {
            var logger = serviceProvider.GetService<ILogger<RedisModuleInitializer>>();
            try
            {
                logger?.LogInformation("Attempting to connect to Redis: {ConnectionString}", redisConnectionString);
                var multiplexer = ConnectionMultiplexer.Connect(configurationOptions);

                if (multiplexer.IsConnected)
                {
                    logger?.LogInformation("Successfully connected to Redis");
                    
                    
                    multiplexer.ConnectionFailed += (sender, args) =>
                    {
                        logger?.LogWarning("Redis connection failed to {EndPoint}: {Exception}", args.EndPoint, args.Exception?.Message);
                    };

                    multiplexer.ConnectionRestored += (sender, args) =>
                    {
                        logger?.LogInformation("Redis connection restored to {EndPoint}", args.EndPoint);
                    };

                    return multiplexer;
                }
                else
                {
                    logger?.LogWarning("Redis connection not available");
                    return null;
                }
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Redis not available at {ConnectionString}, continuing without Redis", redisConnectionString);
                return null; 
            }
        });


        builder.Services.AddScoped<IEventPublisher>(serviceProvider =>
        {
            var redis = serviceProvider.GetService<IConnectionMultiplexer>();
            var redisPubLogger = serviceProvider.GetRequiredService<ILogger<RedisPubSubEventPublisher>>();

            if (redis != null && redis.IsConnected)
            {
                redisPubLogger.LogInformation("Using RedisPubSubEventPublisher");
                return new RedisPubSubEventPublisher(redisPubLogger, redis);
            }

            var fallbackLogger = serviceProvider.GetRequiredService<ILogger<RedisPubSubEventPublisher>>();
            fallbackLogger.LogWarning("Redis not available. Falling back to in-memory publisher.");
            return new RedisPubSubEventPublisher(fallbackLogger, redis);
        });
    }
}