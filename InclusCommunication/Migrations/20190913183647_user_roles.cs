using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InclusCommunication.Migrations
{
    public partial class user_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "users",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "SERIAL", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "VARCHAR(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.id);
                });

            migrationBuilder.InsertData("user_roles", "name", new string[3] { "user", "moderator", "administrator" });
            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "user_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_roles_role_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropIndex(
                name: "IX_users_role_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "users");
        }
    }
}
