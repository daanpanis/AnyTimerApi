using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTimerApi.Database.Migrations
{
    public partial class AddedtimefieldinFriendRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_time",
                table: "friend_requests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_time",
                table: "friend_requests");
        }
    }
}
