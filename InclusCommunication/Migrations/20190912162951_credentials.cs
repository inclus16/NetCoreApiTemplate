using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InclusCommunication.Migrations
{
    public partial class credentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    id = table.Column<int>(type: "SERIAL")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    login = table.Column<string>(type: "VARCHAR(100)"),
                    password = table.Column<string>(type: "VARCHAR(256)"),
                    user_id = table.Column<int>(type: "INT")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credentials", x => x.id);
                    table.ForeignKey(
                        name: "FK_credentials_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade,
                        onUpdate:ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_credentials_user_id",
                table: "credentials",
                column: "user_id");

            migrationBuilder.AddUniqueConstraint("UQ_credentials_login", "credentials", "login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credentials");
        }
    }
}
