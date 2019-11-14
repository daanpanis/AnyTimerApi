using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTimerApi.Database.Migrations
{
    public partial class Initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "any_timers",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    created_time = table.Column<DateTime>(nullable: false),
                    last_updated = table.Column<DateTime>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    receiver_id = table.Column<string>(nullable: false),
                    reason = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_any_timers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "friend_requests",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    requester_id = table.Column<string>(nullable: false),
                    requested_id = table.Column<string>(nullable: false),
                    created_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_friend_requests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "any_timer_senders",
                columns: table => new
                {
                    any_timer_id = table.Column<string>(nullable: false),
                    sender_id = table.Column<string>(nullable: false),
                    amount = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_any_timer_senders", x => new { x.any_timer_id, x.sender_id });
                    table.ForeignKey(
                        name: "fk_any_timer_senders_any_timers_any_timer_id",
                        column: x => x.any_timer_id,
                        principalTable: "any_timers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    any_timer_id = table.Column<string>(nullable: false),
                    user_id = table.Column<string>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    text = table.Column<string>(maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => new { x.any_timer_id, x.user_id, x.time });
                    table.UniqueConstraint("pk_comments", x => new { x.any_timer_id, x.time, x.user_id });
                    table.ForeignKey(
                        name: "fk_comments_any_timers_any_timer_id",
                        column: x => x.any_timer_id,
                        principalTable: "any_timers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "status_events",
                columns: table => new
                {
                    any_timer_id = table.Column<string>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    event_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_status_events", x => new { x.any_timer_id, x.status });
                    table.ForeignKey(
                        name: "fk_status_events_any_timers_any_timer_id",
                        column: x => x.any_timer_id,
                        principalTable: "any_timers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "any_timer_senders");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "friend_requests");

            migrationBuilder.DropTable(
                name: "status_events");

            migrationBuilder.DropTable(
                name: "any_timers");
        }
    }
}
