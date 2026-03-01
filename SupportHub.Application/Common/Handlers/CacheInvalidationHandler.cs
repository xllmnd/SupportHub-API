using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SupportHub.Application.Common.Events;

public class CacheInvalidationHandler : INotificationHandler<CacheInvalidationEvent>
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CacheInvalidationHandler> _logger;

    public CacheInvalidationHandler(IMemoryCache cache, ILogger<CacheInvalidationHandler> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task Handle(CacheInvalidationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("--- Cache REMOVE: {CacheKey} ---", notification.CacheKey);
        _cache.Remove(notification.CacheKey);
        await Task.CompletedTask;
    }
}