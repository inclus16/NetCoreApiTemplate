using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.Entities
{
    [Table("user_statuses")]
    public class UserStatus
    {
        public const int ACTIVE = 1;

        public const int BLOCKED = 2;

        [Column("id", TypeName = "SERIAL")]
        public int Id { get; set; }

        [Column("name", TypeName = "VARCHAR(30)")]
        public string Name { get; set; }

    }
}
