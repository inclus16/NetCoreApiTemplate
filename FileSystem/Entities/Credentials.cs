using FileSystem.Http.Requests;
using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Entities
{
    [Table("credentials")]
    public class Credentials
    {
        [Column("id", TypeName = "SERIAL")]
        public int Id { get; set; }

        [Column("login",TypeName = "VARCHAR(100)")]
        public string Login { get; set; }

        [Column("password",TypeName = "VARCHAR(256)")]
        public string Password { get; set; }

        [Column("user_id",TypeName ="INT")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        private Credentials() { }

        public static Credentials CreateFromRegistration(RegistrationRequest request)
        {
            Credentials credentials = new Credentials
            {
                Login = request.Login,
                Password = Argon2.Hash(request.Password)
            };
            return credentials;
        }
    }
}
