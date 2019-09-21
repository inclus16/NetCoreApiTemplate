using CommandDotNet;
using CommandDotNet.Attributes;
using InclusCommunication.Cli.Models;
using InclusCommunication.Entities;
using InclusCommunication.Services.Implementations;
using InclusCommunication.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.Cli
{
    public class CliInterface
    {
        [InjectProperty]
        public IRepository<User> Users { get; set; }

        [InjectProperty]
        public IRepository<Credentials> Credentials { get; set; }

        [InjectProperty]
        public Postgres Db { get; set; }

        private List<ValidationResult> Errors= new List<ValidationResult>();

        [ApplicationMetadata(Name = "administrator-create", Description = "Creates administrator")]
        public void CreateAdministrator(RegistrateAdministrator model)
        {
            Validate(model);
            if (!IsValid())
            {
                PrintErrors();
                return;
            }

            User user = User.CreateAdministratorFromCli(model);
            using (IDbContextTransaction transaction = Db.Database.BeginTransaction())
            {
                Users.Insert(user);
                InclusCommunication.Entities.Credentials credentials = InclusCommunication.Entities.Credentials.CreateFromCliModel(model);
                credentials.UserId = user.Id;
                Credentials.Insert(credentials);
                transaction.Commit();
            }
            PrinInfo("Successfully create administrator");
            

        }

        private void PrinInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void Validate(IArgumentModel model)
        {
            ValidationContext context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, Errors, true);
            if(model.GetType().Name== "RegistrateAdministrator")
            {
                RegistrateAdministrator modelReg = model as RegistrateAdministrator;
                if (Users.First(x => x.Email == modelReg.Email)!=null)
                {
                    Errors.Add(new ValidationResult("This email is already in use"));
                }
                if (Credentials.First(x => x.Login == modelReg.Login) != null)
                {
                    Errors.Add(new ValidationResult("This login is already in use"));
                }
            }
        }

        private bool IsValid()
        {
            return Errors.Count == 0;
        }

        private void PrintErrors()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach(ValidationResult result in Errors)
            {
                Console.WriteLine(result.ErrorMessage);
            }
            Console.ResetColor();
        }
    }
}
