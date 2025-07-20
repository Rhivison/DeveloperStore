using StackExchange.Redis;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class RedisPubSubEventPublisher : IEventPublisher
    {
        private readonly ILogger<RedisPubSubEventPublisher> _logger;
        private readonly IConnectionMultiplexer _redis;
        private readonly ISubscriber _subscriber;

        public RedisPubSubEventPublisher(ILogger<RedisPubSubEventPublisher> logger, IConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
            _subscriber = _redis?.GetSubscriber();
            _logger?.LogInformation("RedisPubSubEventPublisher criado. Redis IsConnected: {IsConnected}", _redis?.IsConnected);

             if (_subscriber == null)
            {
                _logger?.LogInformation("Subscriber Redis está nulo.");
            }
        }

        public async Task PublishAsync<T>(string channel, T @event) where T : class
        {
            try
            {   
                _logger?.LogInformation("Publishing event: {EventType} to channel {Channel}", typeof(T).Name, channel);

                if (_subscriber != null)
                {
                    var eventData = JsonSerializer.Serialize(@event, new JsonSerializerOptions 
                    { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                    });
                    _logger?.LogInformation("Serialized event data: {EventData}", eventData);
                    _logger?.LogInformation("Publishing to Redis. Channel: {Channel}, Redis Connected: {Connected}", channel, _redis?.IsConnected);

                    long result = await _subscriber.PublishAsync(channel, eventData);

                     _logger?.LogInformation("Event published to Redis. Subscribers that received the message: {Result}", result);
                }
                else
                {
                    _logger?.LogInformation("Redis not available. Event logged only: {EventType} to channel {Channel}", typeof(T).Name, channel);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogInformation(ex, "Failed to publish event: {EventType} to channel {Channel}", typeof(T).Name, channel);
                Console.WriteLine("Erro ao publicar no Redis: " + ex); 
                throw;
            }
        }

    }
}