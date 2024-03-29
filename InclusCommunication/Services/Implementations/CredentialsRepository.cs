﻿using InclusCommunication.Entities;
using InclusCommunication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InclusCommunication.Services.Implementations
{
    public class CredentialsRepository : AbstractRepository, IRepository<Credentials>
    {

        public CredentialsRepository(Postgres db) : base(db) { }
        public Credentials[] All()
        {
            return Db.Credentials.Include(x => x.User).Include(x=>x.User.Role).ToArray();
        }

        public Credentials[] Find(Func<Credentials, bool> predicate)
        {
            return Db.Credentials.Include(x=>x.User).Include(x => x.User.Role).Where(predicate).ToArray();
        }

        public Credentials First(Func<Credentials, bool> predicate)
        {
            return Db.Credentials.Include(x => x.User).Include(x => x.User.Role).FirstOrDefault(predicate);
        }

        public void Insert(Credentials entity)
        {
            Db.Credentials.Add(entity);
            Db.SaveChanges();
        }

        public void InsertRange(IEnumerable<Credentials> entities)
        {
            Db.Credentials.AddRange(entities);
            Db.SaveChanges();
        }

        public void Remove(Credentials entity)
        {
            Db.Credentials.Remove(entity);
            Db.SaveChanges();
        }

        public void RemoveRange(IEnumerable<Credentials> entities)
        {
            Db.Credentials.RemoveRange(entities);
            Db.SaveChanges();
        }

        public void Update(Credentials entity)
        {
            Db.Credentials.Update(entity);
            Db.SaveChanges();

        }
    }
}
