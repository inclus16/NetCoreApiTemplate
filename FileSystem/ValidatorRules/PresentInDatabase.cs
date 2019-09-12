using FileSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileSystem.ValidatorRules
{

    public class PresentInDatabase : ValidationAttribute
    {
        private readonly string Table;

        private readonly string Column;
        public PresentInDatabase(string table, string column)
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
                if (data == null)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;

        }

        private string GetErrorMessage()
        {
            return "Value is invalid";
        }
    }
}
