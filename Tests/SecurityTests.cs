using InclusCommunication;
using InclusCommunication.Entities;
using InclusCommunication.Http.Requests;
using InclusCommunication.Http.Responses;
using InclusCommunication.Services.Implementations;
using InclusCommunication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Tests.Helpers;
using Xunit;

namespace Tests
{
    public class SecurityTests : IClassFixture<FakeWebFactory<InclusCommunication.Startup>>
    {

        protected const string TEST_EMAIL = "test@mail.ru";

        protected const string TEST_LOGIN = "testLogin";

        protected const string TEST_PASSWORD = "123456";


        private readonly FakeWebFactory<Startup> Factory;
        public SecurityTests(FakeWebFactory<Startup> factory) 
        {
            Factory = factory;
        }

        [Fact]
        public async Task TestVerifyUser()
        {
            HttpClient client = Factory.CreateClient();
            
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new LoginRequest
            {
                Login = FakeWebFactory<Startup>.TEST_LOGIN,
                Password = FakeWebFactory<Startup>.TEST_PASSWORD
            }));
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync("/api/security", httpContent);
            response.EnsureSuccessStatusCode();
            Assert.True(JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync())["token"].Length > 10);
            Factory.DestroyDatabase();
        }
    }
}
