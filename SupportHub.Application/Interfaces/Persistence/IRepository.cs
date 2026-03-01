using SupportHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Interfaces.Persistence
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<T?> GetEntityWithSpec(Expression<Func<T, bool>> filter,params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyList<T>> GetEntitiesWithSpec(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    }
}
