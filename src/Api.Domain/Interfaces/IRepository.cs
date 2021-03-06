using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
         Task<T> InsertAsync (T item);
         Task<T> UpdateAsync (T item);
         Task<bool> DeletetAsync (Guid Id);
         Task<T> SelectAsync (Guid Id);
         Task<IEnumerable<T>> SelectAllAsync ();
         Task<bool> ExistAsync(Guid Id);
    }
}