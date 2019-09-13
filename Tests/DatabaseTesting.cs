using InclusCommunication.Cli.Helpers;
using InclusCommunication.Entities;
using InclusCommunication.Services.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using InclusCommunication.Services.Implementations;

namespace Tests
{
    public abstract class DatabaseTesting
    {
        protected const string TEST_EMAIL = "test@mail.ru";

        protected const string TEST_LOGIN = "testLogin";

        protected const string TEST_PASSWORD = "123456";

        protected readonly IRepository<User> Users;

        protected readonly IRepository<Credentials> Credentials;

        public DatabaseTesting()
        {

            IServiceProvider services = ServiceBuilder.BuildServiceProvider();
            Users = services.GetRequiredService<IRepository<User>>();
            Credentials = services.GetRequiredService<IRepository<Credentials>>();
        }

        protected void RemoveTestCredentials()
        {
            Credentials[] credentialsEntities = Credentials.Find(x => x.Login == TEST_LOGIN);
            if (credentialsEntities.Length > 0)
            {
                Credentials.RemoveRange(credentialsEntities);
            }
        }

        protected void RemoveTestUsers()
        {
            User[] existingUsers = Users.Find(x => x.Email == TEST_EMAIL);
            if (existingUsers.Length > 0)
            {
                Users.RemoveRange(existingUsers);
            }
        }
    }
}
