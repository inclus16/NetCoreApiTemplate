using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Services.Interfaces
{
    public interface IRepository<T>
    {
        T[]  All();

        T First(Func<T,bool> predicate);

        T[] Find(Func<T, bool> predicate);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

        void Insert(T entity);

        void InsertRange(IEnumerable<T>  entities);
    }
}
