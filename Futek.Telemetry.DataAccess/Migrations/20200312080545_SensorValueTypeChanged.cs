using Microsoft.EntityFrameworkCore.Migrations;

namespace Futek.Telemetry.DataAccess.Migrations
{
    public partial class SensorValueTypeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "SensorValues",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateIndex(
                name: "IX_SensorValues_SensorId",
                table: "SensorValues",
                column: "SensorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorValues_Sensors_SensorId",
                table: "SensorValues",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorValues_Sensors_SensorId",
                table: "SensorValues");

            migrationBuilder.DropIndex(
                name: "IX_SensorValues_SensorId",
                table: "SensorValues");

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "SensorValues",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
