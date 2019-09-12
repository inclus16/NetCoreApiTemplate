using FileSystem.Cli.Models;
using FileSystem.Http.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Entities
{
    [Table("users")]
    public class User
    {
        [Column("id",TypeName = "SERIAL")]
        public int Id { get; set; }

        [Column("name",TypeName = "VARCHAR(100)")]
        public string Name { get; set; }

        [Column("email",TypeName ="VARCHAR(100)")]
        public string Email { get; set; }

        [Column("status_id",TypeName = "INT")]
        public int StatusId { get; set; }

        [Column("is_admin",TypeName ="BOOLEAN")]
        public bool IsAdmin { get; set; }

        [ForeignKey("StatusId")]
        public UserStatus Status { get; set; }

        [Column("created_at",TypeName ="TIMESTAMP")]
        public DateTime CreatedAt { get; set; }

        private User() { }

        public static User CreateFromRequest(RegistrationRequest request)
        {
            User user = new User
            {
                Name = request.Name,
                Email = request.Email,
                StatusId = UserStatus.WAIT_FOR_EMAIL,
                CreatedAt = DateTime.Now,
                IsAdmin = false
            };
            return user;
            
        }

        public static User CreateAdministratorFromCli(RegistrateAdministrator  cliModel)
        {
            User user = new User
            {
                Name = cliModel.Name,
                Email = cliModel.Email,
                StatusId = UserStatus.ACTIVE,
                CreatedAt = DateTime.Now,
                IsAdmin = true
            };
            return user;
        }
    }
}
