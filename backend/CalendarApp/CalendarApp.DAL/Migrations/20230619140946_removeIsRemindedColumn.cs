using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarApp.DAL.Migrations
{
    public partial class removeIsRemindedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReminded",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReminded",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
