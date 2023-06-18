using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExercicioWebAPI.Migrations.IdentityData
{
    /// <inheritdoc />
    public partial class CreateRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES ('1', 'ADMIN', 'ADMIN', '" + Guid.NewGuid().ToString() + "')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES ('2', 'USER', 'USER', '" + Guid.NewGuid().ToString() + "')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
