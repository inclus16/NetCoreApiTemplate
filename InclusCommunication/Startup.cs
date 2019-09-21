using InclusCommunication.Entities;
using InclusCommunication.Services.Implementations;
using InclusCommunication.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InclusCommunication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Postgres>(x=>
            {
                x.UseNpgsql(Configuration .GetConnectionString("Postgres"));
            });
            services.AddMemoryCache();
            services.AddTransient<IRepository<User>,UsersRepository>();
            services.AddTransient<IRepository<UserStatus>,UserStatusesRepository>();
            services.AddTransient<IRepository<Credentials>, CredentialsRepository>();
            services.AddTransient<IRepository<UserRole>,UserRolesRepository>();
            services.AddTransient<SecurityProvider>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateLifetime = true,
                            ValidateAudience=false,
                            ValidateIssuer=true,
                            ValidIssuer = Configuration.GetValue<string>("JwtIssuer"),
                            // установка ключа безопасности
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("JwtToken")),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true
                        };
                    });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
