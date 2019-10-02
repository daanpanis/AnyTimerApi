using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnyTimerApi.Database.Migrations
{
    public partial class Initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "any_timers",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    amount = table.Column<uint>(nullable: false),
                    requester_id = table.Column<string>(nullable: true),
                    receiver_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_any_timers", x => x.id);
                    table.ForeignKey(
                        name: "fk_any_timers_users_receiver_id",
                        column: x => x.receiver_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_any_timers_users_requester_id",
                        column: x => x.requester_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_any_timers_receiver_id",
                table: "any_timers",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "ix_any_timers_requester_id",
                table: "any_timers",
                column: "requester_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "any_timers");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
