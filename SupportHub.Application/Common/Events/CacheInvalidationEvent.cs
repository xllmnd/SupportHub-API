using MediatR;

namespace SupportHub.Application.Common.Events;


public record CacheInvalidationEvent(string CacheKey) : INotification;