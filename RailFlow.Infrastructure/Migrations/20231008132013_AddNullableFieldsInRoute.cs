using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableFieldsInRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_EndStationId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_StartStationId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Trains_TrainId",
                table: "Routes");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stations_EndStationId",
                table: "Routes",
                column: "EndStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stations_StartStationId",
                table: "Routes",
                column: "StartStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Trains_TrainId",
                table: "Routes",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_EndStationId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Stations_StartStationId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Trains_TrainId",
                table: "Routes");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stations_EndStationId",
                table: "Routes",
                column: "EndStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Stations_StartStationId",
                table: "Routes",
                column: "StartStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Trains_TrainId",
                table: "Routes",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
