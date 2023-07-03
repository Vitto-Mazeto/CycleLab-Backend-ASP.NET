using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations.ExcWebAPI
{
    /// <inheritdoc />
    public partial class NumerodeAmostraToNumeroDeRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nome",
                table: "amostra",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "numero_de_exames",
                table: "amostra",
                newName: "NumeroDeRegistro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "amostra",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "NumeroDeRegistro",
                table: "amostra",
                newName: "numero_de_exames");
        }
    }
}
