using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RailFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Routes_TrainId",
                table: "Routes");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Routes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TrainId",
                table: "Routes",
                column: "TrainId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Routes_TrainId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Routes");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_TrainId",
                table: "Routes",
                column: "TrainId");
        }
    }
}
