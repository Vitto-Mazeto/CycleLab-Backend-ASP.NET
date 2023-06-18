using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExercicioWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "amostra");

            migrationBuilder.AddPrimaryKey(
                name: "PK_amostra",
                table: "amostra",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_amostra",
                table: "amostra");

            migrationBuilder.RenameTable(
                name: "amostra",
                newName: "usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "id");
        }
    }
}
