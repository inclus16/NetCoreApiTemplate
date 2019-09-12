using System;
using Xunit;
using FileSystem;
using FileSystem.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Tests.Helpers;
using FileSystem.Entities;
using FileSystem.Services.Interfaces;

namespace Tests
{
    public class RepositoriesTests
    {
        private readonly IServiceProvider Services;

        public RepositoriesTests()
        {
            Services = ServiceBuilder.BuildServiceProvider();
        }


        [Fact]
        public void TestUsers()
        {
            IRepository<User> users = Services.GetService<IRepository<User>>();
            FileSystem.Http.Requests.RegistrationRequest request = new FileSystem.Http.Requests.RegistrationRequest
            {
                Name = "test",
                Email = "test"
            };
            RemoveTestUsers(users, request);
           
            User user = User.CreateFromRequest(request);
            users.Insert(user);
            Assert.NotEmpty(users.All());
            Assert.NotEmpty(users.Find(x => x.Name == request.Name && x.Email == request.Email));
            Assert.NotNull(users.Find(x => x.Id == user.Id));
            users.Remove(user);
            Assert.Null(users.First(x => x.Id == user.Id));
            Assert.Empty(users.Find(x => x.Name == request.Name && x.Email == request.Email));
            users.Insert(user);
            user.Name = "test2";
            users.Update(user);
            Assert.NotNull(users.Find(x => x.Name == "test2" && x.Email == request.Email));
            users.Remove(user);
        }

        [Fact]
        public void TestCredentials()
        {
            IRepository<Credentials> credentials = Services.GetService<IRepository<Credentials>>();
            IRepository<User> users = Services.GetService<IRepository<User>>();
            var request = new FileSystem.Http.Requests.RegistrationRequest
            {
                Name = "test",
                Email = "test",
                Login="test",
                Password ="test"
            };
            RemoveTestUsers(users, request);
            User user = User.CreateFromRequest(request);
            users.Insert(user);
            Credentials credentialsEntity = Credentials.CreateFromRegistration(request);
            credentialsEntity.UserId = user.Id;
            credentials.Insert(credentialsEntity);
            Assert.NotEmpty(credentials.All());
            Assert.NotEmpty(credentials.Find(x => x.Id == credentialsEntity.Id));
            Assert.NotNull(credentials.First(x => x.Login == request.Login));
            credentials.Remove(credentialsEntity);
            Assert.Empty(credentials.Find(x => x.Login == request.Login));
            users.Remove(user);
        }

        [Fact]
        public void TestUserStatuses()
        {
            IRepository<UserStatus> statuses = Services.GetService<IRepository<UserStatus>>();
            Assert.NotEmpty(statuses.All());
            Assert.NotNull(statuses.Find(x => x.Id == UserStatus.ACTIVE));
            Assert.NotNull(statuses.Find(x => x.Id == UserStatus.WAIT_FOR_EMAIL));
            Assert.NotNull(statuses.Find(x => x.Id == UserStatus.BLOCKED));
        }

        private void RemoveTestUsers(IRepository<User> users, FileSystem.Http.Requests.RegistrationRequest request)
        {
            User[] existingUsers = users.Find(x => x.Name == request.Name && x.Email == request.Email);
            if (existingUsers.Length > 0)
            {
                users.RemoveRange(existingUsers);
            }
        }
    }
}
