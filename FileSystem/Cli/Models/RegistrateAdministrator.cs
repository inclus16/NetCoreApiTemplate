using CommandDotNet;
using InclusCommunication.Http.Requests;
using InclusCommunication.ValidatorRules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.Cli.Models
{
    public class RegistrateAdministrator : IArgumentModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 6)]
        public string Password { get; set; }
    }
}

