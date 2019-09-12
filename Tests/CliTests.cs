using System;
using System.Collections.Generic;
using System.Text;
using FileSystem;
using  FileSystem.Cli.Helpers;
using FileSystem.Entities;
using FileSystem.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tests
{
    public class CliTests
    {
        private readonly IServiceProvider Services;

        private const string TEST_EMAIL= "test@mail.ru";

        private const string TEST_LOGIN= "testLogin";

        public CliTests()
        {
            Services = ServiceBuilder.BuildServiceProvider();
        }

        [Fact]
        public void CreateAdministratorTest()
        {
            IRepository<User> users = Services.GetService<IRepository<User>>();
            IRepository<Credentials> credentials = Services.GetService<IRepository<Credentials>>();
            RemoveTestCredentials(credentials);
            RemoveTestUsers(users);
            Program.HandleCli(new string[5] { "administrator-create", "testName",TEST_EMAIL, TEST_LOGIN, "testPassword" });
            User[] usersTest = users.All();
            Assert.NotNull(users.First(x => x.Email == TEST_EMAIL && x.IsAdmin == true&& x.StatusId==UserStatus.ACTIVE));
            Assert.NotNull(credentials.First(x=>x.Login==TEST_LOGIN));
            RemoveTestCredentials(credentials);
            RemoveTestUsers(users);
        }

        [Fact]
        public void UniqueValidationTest()
        {
            IRepository<User> users = Services.GetService<IRepository<User>>();
            IRepository<Credentials> credentials = Services.GetService<IRepository<Credentials>>();
            RemoveTestCredentials(credentials);
            RemoveTestUsers(users);
            Program.HandleCli(new string[5] { "administrator-create", "testName", TEST_EMAIL, TEST_LOGIN, "testPassword" });
            Assert.True(users.Find(x => x.Email == TEST_EMAIL).Length == 1);
            Assert.True(credentials.Find(x => x.Login == TEST_LOGIN).Length == 1);
            Program.HandleCli(new string[5] { "administrator-create", "testName", TEST_EMAIL, TEST_LOGIN, "testPassword" });
            Assert.True(users.Find(x => x.Email == TEST_EMAIL).Length == 1);
            Assert.True(credentials.Find(x => x.Login == TEST_LOGIN).Length == 1);
            RemoveTestCredentials(credentials);
            RemoveTestUsers(users);
        }

        private void RemoveTestCredentials(IRepository<Credentials> credentials)
        {
            Credentials[] credentialsEntities = credentials.Find(x => x.Login == TEST_LOGIN);
            if (credentialsEntities.Length > 0)
            {
                credentials.RemoveRange(credentialsEntities);
            }
        }

        private void RemoveTestUsers(IRepository<User> users)
        {
            User[] existingUsers = users.Find(x =>  x.Email == TEST_EMAIL);
            if (existingUsers.Length > 0)
            {
                users.RemoveRange(existingUsers);
            }
        }

    }
}
