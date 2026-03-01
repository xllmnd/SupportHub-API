using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SupportHub.Application.Common.Interfaces;

namespace SupportHub.Application.Common.Behaviors
{
    

    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

        public CachingBehavior(IMemoryCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_cache.TryGetValue(request.CacheKey, out TResponse cachedResponse))
            {
                _logger.LogInformation("--- Cache HIT: {CacheKey} ---", request.CacheKey);
                return cachedResponse;
            }

            _logger.LogInformation("--- Cache MISS: {CacheKey} ---", request.CacheKey);
            var response = await next();

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = request.Expiration ?? TimeSpan.FromMinutes(10)
            };

            _cache.Set(request.CacheKey, response, cacheOptions);

            return response;
        }
    }
}
