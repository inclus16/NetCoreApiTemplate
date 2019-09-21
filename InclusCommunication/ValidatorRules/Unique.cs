
using InclusCommunication.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.ValidatorRules
{
    public class Unique:ValidationAttribute
    {
        private readonly string Table;

        private readonly string Column;

        public Unique(string table, string column)
        {
            Table = table;
            Column = column;
        }


        protected override ValidationResult IsValid(
           object value, ValidationContext validationContext)
        {
            Postgres db = (Postgres)validationContext.GetService(typeof(Postgres));
            NpgsqlConnection connection = db.Database.GetDbConnection() as NpgsqlConnection;

            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            using (NpgsqlCommand command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT id FROM {Table} WHERE {Column}=@value LIMIT 1";
                command.Parameters.AddWithValue("@value", value);
                command.Prepare();
                object data = command.ExecuteScalar();
                if (data != null)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;

        }

        private string GetErrorMessage()
        {
            return $"This {Column} is already in use";
        }
    }
}
