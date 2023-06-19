using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarApp.DAL.Migrations
{
    public partial class UpdateRemainingTimeAsRemindDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemaningTime",
                table: "Events");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RemindDate",
                table: "Events",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemindDate",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "RemaningTime",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
