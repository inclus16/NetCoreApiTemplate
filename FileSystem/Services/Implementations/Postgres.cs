﻿using FileSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Services.Implementations
{
    public class Postgres:DbContext
    {
        private string ConnectionString;
        public Postgres(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Postgres");
        }

            public DbSet<Credentials> Credentials { get; set; }

             public DbSet<User> Users { get; set; }

            public DbSet<UserStatus> UserStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}