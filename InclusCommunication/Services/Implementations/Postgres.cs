using InclusCommunication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.Services.Implementations
{
    public class Postgres : DbContext
    {
        public Postgres(DbContextOptions<Postgres> options)
        : base(options)
        { }

        public DbSet<Credentials> Credentials { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserStatus> UserStatuses { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

    }
}
