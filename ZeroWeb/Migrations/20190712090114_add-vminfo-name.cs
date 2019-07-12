using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeroWeb.Migrations
{
    public partial class addvminfoname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "VmInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "VmInfos");
        }
    }
}
