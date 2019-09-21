using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.Entities
{
    [Table("user_roles")]
    public class UserRole
    {
        public const int USER = 1;

        public const int MODERATOR = 2;

        public const int ADMINISTRATOR = 3;

        [Column("id",TypeName = "SERIAL")]
        public int Id { get; set; }

        [Column("name",TypeName = "VARCHAR(20)")]
        public string Name { get; set; }
    }
}
