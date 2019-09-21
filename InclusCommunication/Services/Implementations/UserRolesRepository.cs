using InclusCommunication.Entities;
using InclusCommunication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.Services.Implementations
{
    public class UserRolesRepository :AbstractRepository, IRepository<UserRole>
    {
        public UserRolesRepository(Postgres db) : base(db) { }
        public UserRole[] All()
        {
            return Db.UserRoles.ToArray();
        }

        public UserRole[] Find(Func<UserRole, bool> predicate)
        {
            return Db.UserRoles.Where(predicate).ToArray();
        }

        public UserRole First(Func<UserRole, bool> predicate)
        {
            return Db.UserRoles.FirstOrDefault(predicate);
        }

        public void Insert(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(IEnumerable<UserRole> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<UserRole> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
