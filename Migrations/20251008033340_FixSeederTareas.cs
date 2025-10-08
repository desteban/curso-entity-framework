using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entity_framework.Migrations
{
    /// <inheritdoc />
    public partial class FixSeederTareas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tarea",
                keyColumn: "tareaId",
                keyValue: new Guid("2afee021-4f8a-4a60-9824-1703695b23c3"),
                column: "creacion",
                value: new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tarea",
                keyColumn: "tareaId",
                keyValue: new Guid("2afee021-4f8a-4a60-9824-1703695b23c3"),
                column: "creacion",
                value: new DateTime(2025, 10, 7, 22, 26, 16, 962, DateTimeKind.Local).AddTicks(2688));
        }
    }
}
