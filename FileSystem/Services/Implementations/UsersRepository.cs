﻿using FileSystem.Entities;
using FileSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Services.Implementations
{
    public class UsersRepository : AbstractRepository, IRepository<User>
    {

        public UsersRepository(Postgres db) : base(db) { }
        public User[] All()
        {
            return  Db.Users.ToArray();
        }

        public User[] Find(Func<User, bool> predicate)
        {
           return Db.Users.Where(predicate).ToArray();
        }

        public User First(Func<User, bool> predicate)
        {
            return Db.Users.FirstOrDefault(predicate);
        }

        public void Insert(User entity)
        {
            Db.Users.Add(entity);
            Db.SaveChanges();
        }

        public void InsertRange(IEnumerable<User> entities)
        {
            Db.Users.AddRange(entities);
            Db.SaveChanges();
        }

        public void Remove(User entity)
        {
            Db.Users.Remove(entity);
            Db.SaveChanges();
        }

        public void RemoveRange(IEnumerable<User> entities)
        {
            Db.Users.RemoveRange(entities);
            Db.SaveChanges();
        }

        public void Update(User entity)
        {
            Db.Users.Update(entity);
            Db.SaveChanges();
        }
    }
}
