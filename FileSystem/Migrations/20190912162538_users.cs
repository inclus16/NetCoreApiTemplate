using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FileSystem.Migrations
{
    public partial class users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "SERIAL", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "VARCHAR(30)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_statuses", x => x.id);
                });

            migrationBuilder.InsertData("user_statuses", "name", new string[3] { "wait_for_email", "active", "blocked" });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "SERIAL", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "VARCHAR(100)"),
                    email = table.Column<string>(type: "VARCHAR(100)"),
                    status_id = table.Column<int>(type: "INT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP", nullable: true,defaultValueSql:"NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_user_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "user_statuses",
                        principalColumn: "id",
                        onUpdate: ReferentialAction.Cascade);
                });

            migrationBuilder.AddUniqueConstraint("UQ_user_email", "users", "email");

            migrationBuilder.CreateIndex(
                name: "IX_users_status_id",
                table: "users",
                column: "status_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "user_statuses");
        }
    }
}
