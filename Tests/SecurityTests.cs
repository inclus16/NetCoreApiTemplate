using InclusCommunication.Cli.Helpers;
using InclusCommunication.Entities;
using InclusCommunication.Http.Responses;
using InclusCommunication.Services.Implementations;
using InclusCommunication.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using Xunit;

namespace Tests
{
    public class SecurityTests : DatabaseTesting
    {
        private readonly SecurityProvider Security;

        public SecurityTests():base()
        {
            IServiceProvider services= ServiceBuilder.BuildServiceProvider();
            Security = services.GetRequiredService<SecurityProvider>();
          
        }

        [Fact]
        public void TestVerifyUser()
        {
            RemoveTestCredentials();
            RemoveTestUsers();
            var request = new InclusCommunication.Http.Requests.RegistrationRequest
            {
                Name = "test",
                Email = TEST_EMAIL,
                Login = TEST_LOGIN,
                Password = TEST_PASSWORD
            };
            User user = User.CreateFromRequest(request);
            Users.Insert(user);
            Credentials credentialsEntity = InclusCommunication.Entities.Credentials.CreateFromRegistration(request);
            credentialsEntity.UserId = user.Id;
            Credentials.Insert(credentialsEntity);
            AbstractResponse response = Security.GetResponse(new InclusCommunication.Http.Requests.LoginRequest
            {
                Login = TEST_LOGIN,
                Password = TEST_PASSWORD
            });
            Assert.True(response.Success);
            Assert.NotNull(response.Data);
        }
    }
}
