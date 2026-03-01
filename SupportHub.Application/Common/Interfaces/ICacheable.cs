using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Common.Interfaces
{
    public interface ICacheable
    {
        string CacheKey { get; }      
        TimeSpan? Expiration { get; } 
    }
}
