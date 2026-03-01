using Microsoft.EntityFrameworkCore;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;
using SupportHub.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace SupportHub.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SupportHubDbContext _context;
        public Repository(SupportHubDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetEntityWithSpec(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<IReadOnlyList<T>> GetEntitiesWithSpec(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            return await query.Where(filter).ToListAsync();
        }
    }
}
