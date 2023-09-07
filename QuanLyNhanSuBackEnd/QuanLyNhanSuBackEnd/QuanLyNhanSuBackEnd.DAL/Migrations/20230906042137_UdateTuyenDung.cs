using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyNhanSuBackEnd.DAL.Migrations
{
    public partial class UdateTuyenDung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "KetQua",
                table: "tuyenDung",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LienHe",
                table: "tuyenDung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ten",
                table: "tuyenDung",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViTriUngTuyen",
                table: "tuyenDung",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KetQua",
                table: "tuyenDung");

            migrationBuilder.DropColumn(
                name: "LienHe",
                table: "tuyenDung");

            migrationBuilder.DropColumn(
                name: "Ten",
                table: "tuyenDung");

            migrationBuilder.DropColumn(
                name: "ViTriUngTuyen",
                table: "tuyenDung");
        }
    }
}
