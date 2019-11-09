using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTimerApi.Database.Migrations
{
    public partial class RemoveduserentityandswitchedtoFirebasesuserrecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "requester_id",
                table: "friend_requests",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "requested_id",
                table: "friend_requests",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "requester_id",
                table: "friend_requests",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "requested_id",
                table: "friend_requests",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
