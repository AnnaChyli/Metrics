using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Metrics.Domain.Common;

namespace Metrics.Data.Common
{
    public interface IDataContext : IDisposable
    {
        Task<bool> DeleteAsync<T>(T obj) where T : IEntity;
        Task<bool> DeleteAsync<T>(Guid id) where T : IEntity;
        Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> whereClause) where T : IEntity;
        Task<T> GetByIdAsync<T>(Guid id) where T : IEntity;
        Task<Guid> InsertAsync<T>(T obj) where T : IEntity;
        Task<bool> UpdateAsync<T>(T obj) where T : IEntity;
    }
}
