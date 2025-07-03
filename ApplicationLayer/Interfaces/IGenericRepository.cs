using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<OperationResult<T>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<T>>> GetAllAsync();
        Task<OperationResult<T>> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<OperationResult<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<OperationResult<bool>> DeleteByIdAsync(int id);
        Task<OperationResult<bool>> DeleteAllAsync();
        Task<IEnumerable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includes);



        IQueryable<T> AsQueryable();
        //Task<OperationResult<bool>> SaveChangesAsync();
    }
}
