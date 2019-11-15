using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTimerApi.Database.Migrations
{
    public partial class AddedcreatortoAnyTimer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "creator_id",
                table: "any_timers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "any_timers");
        }
    }
}
