using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using InclusCommunication.Cli.Helpers;
using InclusCommunication.Entities;
using InclusCommunication.Services.Interfaces;

namespace Tests
{
    public class RepositoriesTests : DatabaseTesting
    {
        private readonly IServiceProvider Services;

        public RepositoriesTests()
        {

            Services = ServiceBuilder.BuildServiceProvider();
        }


        [Fact]
        public void TestUsers()
        {
            InclusCommunication.Http.Requests.RegistrationRequest request = new InclusCommunication.Http.Requests.RegistrationRequest
            {
                Name = "test",
                Email = TEST_EMAIL
            };
            RemoveTestUsers();
           
            User user = User.CreateFromRequest(request);
            Users.Insert(user);
            Assert.NotEmpty(Users.All());
            Assert.NotEmpty(Users.Find(x => x.Name == request.Name && x.Email == request.Email));
            Assert.NotNull(Users.Find(x => x.Id == user.Id));
            Users.Remove(user);
            Assert.Null(Users.First(x => x.Id == user.Id));
            Assert.Empty(Users.Find(x => x.Name == request.Name && x.Email == request.Email));
            Users.Insert(user);
            user.Name = "test2";
            Users.Update(user);
            Assert.NotNull(Users.Find(x => x.Name == "test2" && x.Email == request.Email));
        }

        [Fact]
        public void TestCredentials()
        {
            var request = new InclusCommunication.Http.Requests.RegistrationRequest
            {
                Name = "test",
                Email = TEST_EMAIL,
                Login=TEST_LOGIN,
                Password = TEST_PASSWORD
            };
            RemoveTestUsers();
            RemoveTestCredentials();
            User user = User.CreateFromRequest(request);
            Users.Insert(user);
            Credentials credentialsEntity = InclusCommunication.Entities.Credentials.CreateFromRegistration(request);
            credentialsEntity.UserId = user.Id;
            Credentials.Insert(credentialsEntity);
            Assert.NotEmpty(Credentials.All());
            Assert.NotEmpty(Credentials.Find(x => x.Id == credentialsEntity.Id));
            Assert.NotNull(Credentials.First(x => x.Login == request.Login));
            Credentials.Remove(credentialsEntity);
            Assert.Empty(Credentials.Find(x => x.Login == request.Login));
        }

        [Fact]
        public void TestUserStatuses()
        {
            IRepository<UserStatus> statuses = Services.GetService<IRepository<UserStatus>>();
            Assert.NotEmpty(statuses.All());
            Assert.NotNull(statuses.Find(x => x.Id == UserStatus.ACTIVE));
            Assert.NotNull(statuses.Find(x => x.Id == UserStatus.BLOCKED));
        }

    }
}
