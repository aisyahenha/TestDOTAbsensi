using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestAbsensi.Migrations
{
    /// <inheritdoc />
    public partial class Initial9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_m_karyawan_NIK",
                table: "m_karyawan",
                column: "NIK",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_m_karyawan_NIK",
                table: "m_karyawan");
        }
    }
}
