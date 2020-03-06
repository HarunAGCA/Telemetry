using Microsoft.EntityFrameworkCore.Migrations;

namespace Futek.Telemetry.DataAccess.Migrations
{
    public partial class deletedsensorNamefromSensorValueclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "SensorValues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "SensorValues",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
