using System;
using System.Collections.Generic;
using System.Text;
using InclusCommunication;
using  InclusCommunication.Cli.Helpers;
using InclusCommunication.Entities;
using InclusCommunication.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tests
{
    public class CliTests : DatabaseTesting
    {

        [Fact]
        public void CreateAdministratorTest()
        {
            RemoveTestCredentials();
            RemoveTestUsers();
            Program.HandleCli(new string[5] { "administrator-create", "testName",TEST_EMAIL, TEST_LOGIN,TEST_PASSWORD });
            User[] usersTest = Users.All();
            Assert.NotNull(Users.First(x => x.Email == TEST_EMAIL && x.RoleId == UserRole.ADMINISTRATOR&& x.StatusId==UserStatus.ACTIVE));
            Assert.NotNull(Credentials.First(x=>x.Login==TEST_LOGIN));
        }

        [Fact]
        public void UniqueValidationTest()
        {
            RemoveTestCredentials();
            RemoveTestUsers();
            Program.HandleCli(new string[5] { "administrator-create", "testName", TEST_EMAIL, TEST_LOGIN, TEST_PASSWORD });
            Assert.True(Users.Find(x => x.Email == TEST_EMAIL).Length == 1);
            Assert.True(Credentials.Find(x => x.Login == TEST_LOGIN).Length == 1);
            Program.HandleCli(new string[5] { "administrator-create", "testName", TEST_EMAIL, TEST_LOGIN, TEST_PASSWORD });
            Assert.True(Users.Find(x => x.Email == TEST_EMAIL).Length == 1);
            Assert.True(Credentials.Find(x => x.Login == TEST_LOGIN).Length == 1);
        }

    }
}
