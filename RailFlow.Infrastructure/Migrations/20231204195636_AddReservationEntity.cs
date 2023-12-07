using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstScheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    SecondScheduleId = table.Column<Guid>(type: "uuid", nullable: true),
                    StartStopId = table.Column<Guid>(type: "uuid", nullable: false),
                    EndStopId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransferStopId = table.Column<Guid>(type: "uuid", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Schedules_FirstScheduleId",
                        column: x => x.FirstScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Schedules_SecondScheduleId",
                        column: x => x.SecondScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Stops_EndStopId",
                        column: x => x.EndStopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Stops_StartStopId",
                        column: x => x.StartStopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Stops_TransferStopId",
                        column: x => x.TransferStopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EndStopId",
                table: "Reservations",
                column: "EndStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FirstScheduleId",
                table: "Reservations",
                column: "FirstScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SecondScheduleId",
                table: "Reservations",
                column: "SecondScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StartStopId",
                table: "Reservations",
                column: "StartStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TransferStopId",
                table: "Reservations",
                column: "TransferStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
