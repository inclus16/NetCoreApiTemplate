using InclusCommunication.Entities;
using InclusCommunication.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.Services.Implementations
{
    public class UserStatusesRepository:AbstractRepository,IRepository<UserStatus>
    {
        private const string CACHE_NAME = "user_roles";

        private readonly IMemoryCache Cache;
        public UserStatusesRepository(Postgres db,IMemoryCache cache) : base(db)
        {
            Cache = cache;
        }

        public UserStatus[] All()
        {
            UserStatus[] statuses;
            if(!Cache.TryGetValue<UserStatus[]>(CACHE_NAME,out statuses))
            {
                SaveStatusesInCacheIfNotStored(out statuses);
            }            
            return statuses;
        }

        public UserStatus[] Find(Func<UserStatus, bool> predicate)
        {
            UserStatus[] statuses;
            SaveStatusesInCacheIfNotStored(out statuses);
            return statuses.Where(predicate).ToArray();
        }

        public UserStatus First(Func<UserStatus, bool> predicate)
        {
            UserStatus[] statuses;
            SaveStatusesInCacheIfNotStored(out statuses);
            return statuses.FirstOrDefault(predicate);
        }

        public void Insert(UserStatus entity)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(IEnumerable<UserStatus> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserStatus entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<UserStatus> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(UserStatus entity)
        {
            throw new NotImplementedException();
        }

        private void SaveStatusesInCacheIfNotStored(out UserStatus[] statuses)
        {
            if (!Cache.TryGetValue<UserStatus[]>(CACHE_NAME, out statuses))
            {
                statuses = Db.UserStatuses.ToArray();
                Cache.Set(CACHE_NAME, statuses);
            }
        }
    }
}
