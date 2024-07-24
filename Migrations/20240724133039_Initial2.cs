using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestAbsensi.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_absensi_m_karyawan_KaryawanModelId",
                table: "t_absensi");

            migrationBuilder.DropIndex(
                name: "IX_t_absensi_KaryawanModelId",
                table: "t_absensi");

            migrationBuilder.DropColumn(
                name: "KaryawanModelId",
                table: "t_absensi");

            migrationBuilder.RenameColumn(
                name: "Karyawan_Id",
                table: "t_absensi",
                newName: "KaryawanId");

            migrationBuilder.CreateIndex(
                name: "IX_t_absensi_KaryawanId",
                table: "t_absensi",
                column: "KaryawanId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_absensi_m_karyawan_KaryawanId",
                table: "t_absensi",
                column: "KaryawanId",
                principalTable: "m_karyawan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_absensi_m_karyawan_KaryawanId",
                table: "t_absensi");

            migrationBuilder.DropIndex(
                name: "IX_t_absensi_KaryawanId",
                table: "t_absensi");

            migrationBuilder.RenameColumn(
                name: "KaryawanId",
                table: "t_absensi",
                newName: "Karyawan_Id");

            migrationBuilder.AddColumn<int>(
                name: "KaryawanModelId",
                table: "t_absensi",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_absensi_KaryawanModelId",
                table: "t_absensi",
                column: "KaryawanModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_absensi_m_karyawan_KaryawanModelId",
                table: "t_absensi",
                column: "KaryawanModelId",
                principalTable: "m_karyawan",
                principalColumn: "Id");
        }
    }
}
