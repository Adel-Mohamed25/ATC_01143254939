using EventBooking.Domain.Enums;
using EventBooking.Domain.IRepositories;
using EventBooking.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventBooking.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IEventBookingDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(IEventBookingDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }


        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public virtual async Task UpdatedRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities is not null)
            {
                _dbSet.UpdateRange(entities);
            }
            await Task.CompletedTask;
        }


        public virtual async Task ExecuteUpdateAsync(Func<TEntity, object> propertySelector, Expression<Func<TEntity, object>> valueSelector, Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            if (filter is null)
                await _dbSet.ExecuteUpdateAsync(entity => entity.SetProperty(propertySelector, valueSelector), cancellationToken);

            else
                await _dbSet.Where(filter).ExecuteUpdateAsync(entity => entity.SetProperty(propertySelector, valueSelector), cancellationToken);

        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(_dbSet.Remove(entity));
        }

        public virtual async Task ExecuteDeleteAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            if (filter is null)
                await _dbSet.ExecuteDeleteAsync(cancellationToken);
            else
                await _dbSet.Where(filter).ExecuteDeleteAsync(cancellationToken);
        }

        public virtual async Task<TEntity>
            GetByAsync(Expression<Func<TEntity, bool>> mandatoryFilter,
                            Expression<Func<TEntity, bool>>? optionalFilter = null,
                            CancellationToken cancellationToken = default,
                            params string[] includes)
        {
            IQueryable<TEntity> entities = _dbSet.AsNoTracking().AsQueryable();

            if (optionalFilter is null)
                entities = entities.Where(mandatoryFilter);
            else
                entities = entities.Where(mandatoryFilter).Where(optionalFilter);

            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                {
                    entities = entities.Include(include);
                }

                entities = entities.AsSplitQuery();
            }

            return await entities.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<IQueryable<TEntity>>
            GetAllAsync(Expression<Func<TEntity, bool>>? firstFilter = null,
                             Expression<Func<TEntity, bool>>? secondFilter = null,
                             Expression<Func<TEntity, object>>? orderBy = null,
                             OrderByDirection orderByDirection = OrderByDirection.Ascending,
                             int? pageNumber = null,
                             int? pageSize = null,
                             bool ispagination = false,
                             CancellationToken cancellationToken = default,
                             params string[] includes)
        {
            IQueryable<TEntity> entities = _dbSet.AsNoTracking().AsQueryable();

            if (firstFilter is not null)
                entities = entities.Where(firstFilter);

            if (secondFilter is not null)
                entities = entities.Where(secondFilter);

            if (orderBy is not null)
            {
                if (orderByDirection == OrderByDirection.Ascending)
                    entities = entities.OrderBy(orderBy);
                else
                    entities = entities.OrderByDescending(orderBy);
            }

            if (ispagination)
            {
                pageNumber = pageNumber.HasValue ? pageNumber.Value <= 0 ? 1 : pageNumber.Value : 1;
                pageSize = pageSize.HasValue ? pageSize.Value <= 0 ? 10 : pageSize.Value : 10;
                entities = entities.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            if (includes?.Length > 0)
            {
                foreach (var include in includes)
                {
                    entities = entities.Include(include);
                }

                entities = entities.AsSplitQuery();
            }


            return await Task.FromResult(entities);
        }



        public virtual async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            if (filter is null)
                return await _dbSet.AnyAsync(cancellationToken);
            return await _dbSet.AnyAsync(filter, cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default)
        {
            if (filter is null)
                return await _dbSet.CountAsync(cancellationToken);
            return await _dbSet.CountAsync(filter, cancellationToken);
        }

    }
}
