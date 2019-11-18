using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTimerApi.Database.Migrations
{
    public partial class ChangedAnyTimerStatusCommentStatusEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "status_events",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "edited",
                table: "comments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "message",
                table: "status_events");

            migrationBuilder.DropColumn(
                name: "edited",
                table: "comments");
        }
    }
}
