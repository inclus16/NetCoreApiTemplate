using System;
using System.Collections.Generic;
using System.Text;
using InclusCommunication;
using InclusCommunication.Entities;
using InclusCommunication.Services.Implementations;
using InclusCommunication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using Xunit;

namespace Tests
{
    public class CliTests : IClassFixture<FakeWebFactory<InclusCommunication.Startup>>
    {

        protected const string TEST_EMAIL = "test@mail.ru";

        protected const string TEST_LOGIN = "testLogin";

        protected const string TEST_PASSWORD = "123456";


        private readonly FakeWebFactory<Startup> Factory;
        public CliTests(FakeWebFactory<Startup> factory)
        {
            Factory = factory;
        }
        [Fact]
        public void CreateAdministratorTest()
        {
            Factory.CreateClient();
            IRepository<User> users = Factory.GetService<IRepository<User>>();
            IRepository<Credentials> credentials = Factory.GetService<IRepository<Credentials>>();
            Program.HandleCli(new string[5] { "administrator-create", "testName",TEST_EMAIL, TEST_LOGIN,TEST_PASSWORD });
            User[] usersTest = users.All();
            Assert.NotNull(users.First(x => x.Email == TEST_EMAIL && x.RoleId == UserRole.ADMINISTRATOR&& x.StatusId==UserStatus.ACTIVE));
            Assert.NotNull(credentials.First(x=>x.Login==TEST_LOGIN));
            Factory.DestroyDatabase();
        }

    }
}
