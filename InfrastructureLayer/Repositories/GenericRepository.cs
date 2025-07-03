using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly WebShopSystemDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(WebShopSystemDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<OperationResult<T>> AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return OperationResult<T>.Success(entity);
            }
            catch (Exception ex)
            {
                // This captures the deeper cause
                return OperationResult<T>.Failure(GetExceptionMessage(ex));
            }
        }

        public async Task<OperationResult<bool>> DeleteByIdAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);

                if (entity == null)
                {
                    return OperationResult<bool>.Failure("Entity not found");
                }
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(GetExceptionMessage(ex));
            }
        }

        public async Task<OperationResult<bool>> DeleteAllAsync()
        {
            try
            {
                _dbSet.RemoveRange(_dbSet);
                await _context.SaveChangesAsync();
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<OperationResult<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                var list = await _dbSet.ToListAsync();
                return OperationResult<IEnumerable<T>>.Success(list);
            }
            catch (Exception ex)

            {
                return OperationResult<IEnumerable<T>>.Failure(GetExceptionMessage(ex));
            }
        }

        public async Task<OperationResult<T>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                return entity != null
                    ? OperationResult<T>.Success(entity)
                    : OperationResult<T>.Failure("Entity not found");
            }
            catch (Exception ex)
            {
                return OperationResult<T>.Failure(GetExceptionMessage(ex));
            }
        }

        public async Task<OperationResult<T>> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return OperationResult<T>.Success(entity);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.Failure(GetExceptionMessage(ex));
            }
        }
        private static string GetExceptionMessage(Exception ex)
        {
            return ex.InnerException?.Message ?? ex.Message;
        }
        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
    }
}
