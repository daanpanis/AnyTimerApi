using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTimerApi.Database.Migrations
{
    public partial class Addedmultiplesendersstatusupdatedandcommentstoanytimers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_any_timers_users_receiver_id",
                table: "any_timers");

            migrationBuilder.DropForeignKey(
                name: "fk_any_timers_users_requester_id",
                table: "any_timers");

            migrationBuilder.DropIndex(
                name: "ix_any_timers_requester_id",
                table: "any_timers");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "any_timers");

            migrationBuilder.DropColumn(
                name: "requester_id",
                table: "any_timers");

            migrationBuilder.AddColumn<string>(
                name: "any_timer_id",
                table: "users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "receiver_id",
                table: "any_timers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "any_timers",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_time",
                table: "any_timers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_updated",
                table: "any_timers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "any_timers",
                nullable: false,
                defaultValue: 0);

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
                    table.ForeignKey(
                        name: "fk_any_timer_senders_users_sender_id",
                        column: x => x.sender_id,
                        principalTable: "users",
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
                    table.PrimaryKey("pk_comments", x => new { x.any_timer_id, x.user_id, x.time });
                    table.UniqueConstraint("ak_comments_any_timer_id_time_user_id", x => new { x.any_timer_id, x.time, x.user_id });
                    table.ForeignKey(
                        name: "fk_comments_any_timers_any_timer_id",
                        column: x => x.any_timer_id,
                        principalTable: "any_timers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "friend_requests",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    requester_id = table.Column<string>(nullable: true),
                    requested_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_friend_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_friend_requests_users_requested_id",
                        column: x => x.requested_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_friend_requests_users_requester_id",
                        column: x => x.requester_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "ix_users_any_timer_id",
                table: "users",
                column: "any_timer_id");

            migrationBuilder.CreateIndex(
                name: "ix_any_timer_senders_sender_id",
                table: "any_timer_senders",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_user_id",
                table: "comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_friend_requests_requested_id",
                table: "friend_requests",
                column: "requested_id");

            migrationBuilder.CreateIndex(
                name: "ix_friend_requests_requester_id",
                table: "friend_requests",
                column: "requester_id");

            migrationBuilder.AddForeignKey(
                name: "fk_any_timers_users_receiver_id",
                table: "any_timers",
                column: "receiver_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_any_timers_any_timer_id",
                table: "users",
                column: "any_timer_id",
                principalTable: "any_timers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_any_timers_users_receiver_id",
                table: "any_timers");

            migrationBuilder.DropForeignKey(
                name: "fk_users_any_timers_any_timer_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "any_timer_senders");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "friend_requests");

            migrationBuilder.DropTable(
                name: "status_events");

            migrationBuilder.DropIndex(
                name: "ix_users_any_timer_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "any_timer_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_time",
                table: "any_timers");

            migrationBuilder.DropColumn(
                name: "last_updated",
                table: "any_timers");

            migrationBuilder.DropColumn(
                name: "status",
                table: "any_timers");

            migrationBuilder.AlterColumn<string>(
                name: "receiver_id",
                table: "any_timers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "any_timers",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<uint>(
                name: "amount",
                table: "any_timers",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<string>(
                name: "requester_id",
                table: "any_timers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_any_timers_requester_id",
                table: "any_timers",
                column: "requester_id");

            migrationBuilder.AddForeignKey(
                name: "fk_any_timers_users_receiver_id",
                table: "any_timers",
                column: "receiver_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_any_timers_users_requester_id",
                table: "any_timers",
                column: "requester_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
