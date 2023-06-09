using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASS_BanHang_Final.Migrations
{
    /// <inheritdoc />
    public partial class updateModelBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Bill",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SDT",
                table: "Bill",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "SDT",
                table: "Bill");
        }
    }
}
