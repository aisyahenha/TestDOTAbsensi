using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestAbsensi.Migrations
{
    /// <inheritdoc />
    public partial class Initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DetailAbsensi",
                table: "t_absensi",
                newName: "StatusMasuk");

            migrationBuilder.AddColumn<string>(
                name: "StatusKeluar",
                table: "t_absensi",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusKeluar",
                table: "t_absensi");

            migrationBuilder.RenameColumn(
                name: "StatusMasuk",
                table: "t_absensi",
                newName: "DetailAbsensi");
        }
    }
}
