
using InclusCommunication.Entities;
using InclusCommunication.Services.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using InclusCommunication.Services.Implementations;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Tests.Helpers;

namespace Tests
{
    public abstract class DatabaseTesting : IClassFixture<FakeWebFactory<InclusCommunication.Startup>>
    {
        protected const string TEST_EMAIL = "test@mail.ru";

        protected const string TEST_LOGIN = "testLogin";

        protected const string TEST_PASSWORD = "123456";


        protected readonly FakeWebFactory<InclusCommunication.Startup> Factory;

        public DatabaseTesting(FakeWebFactory<InclusCommunication.Startup> factory)
        {
            Factory = factory;
        }



    }
}
