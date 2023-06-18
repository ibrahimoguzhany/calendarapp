using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarApp.DAL.Migrations
{
    public partial class add_calendarid_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalendarId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Events");
        }
    }
}
