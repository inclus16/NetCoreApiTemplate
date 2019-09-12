using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.Entities
{
    [Table("user_statuses")]
    public class UserStatus
    {

        public const int WAIT_FOR_EMAIL= 1;

        public const int ACTIVE = 2;

        public const int BLOCKED = 3;

        [Column("id", TypeName = "SERIAL")]
        public int Id { get; set; }

        [Column("name", TypeName = "VARCHAR(30)")]
        public string Name { get; set; }

    }
}
