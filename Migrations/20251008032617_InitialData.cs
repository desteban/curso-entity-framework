using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace entity_framework.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaFinalizacion",
                table: "tarea",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "categoria",
                columns: new[] { "categoriaId", "descripcion", "nombre", "peso" },
                values: new object[,]
                {
                    { new Guid("99eb1cae-a854-4503-a1ac-bf13c2453271"), "Estudiar o practicar alguna habilidad", "Estudiar", 3 },
                    { new Guid("c3db2d1e-b1ff-43c4-acfe-257f49916ae2"), "Tareas que debemos realizar en casa", "En Casa", 2 },
                    { new Guid("f5edfbfd-aa6c-4261-b24a-3ef3c85a5984"), "Tareas que requieren un desgaste físico", "Ejercitar", 5 }
                });

            migrationBuilder.InsertData(
                table: "tarea",
                columns: new[] { "tareaId", "categoiaId", "creacion", "descripcion", "estado", "fechaFinalizacion", "prioridad", "titulo" },
                values: new object[] { new Guid("2afee021-4f8a-4a60-9824-1703695b23c3"), new Guid("99eb1cae-a854-4503-a1ac-bf13c2453271"), new DateTime(2025, 10, 7, 22, 26, 16, 962, DateTimeKind.Local).AddTicks(2688), "Realizar la ruta de desarrollo con .net", 1, null, 2, "Aprender .net" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categoria",
                keyColumn: "categoriaId",
                keyValue: new Guid("c3db2d1e-b1ff-43c4-acfe-257f49916ae2"));

            migrationBuilder.DeleteData(
                table: "categoria",
                keyColumn: "categoriaId",
                keyValue: new Guid("f5edfbfd-aa6c-4261-b24a-3ef3c85a5984"));

            migrationBuilder.DeleteData(
                table: "tarea",
                keyColumn: "tareaId",
                keyValue: new Guid("2afee021-4f8a-4a60-9824-1703695b23c3"));

            migrationBuilder.DeleteData(
                table: "categoria",
                keyColumn: "categoriaId",
                keyValue: new Guid("99eb1cae-a854-4503-a1ac-bf13c2453271"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaFinalizacion",
                table: "tarea",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
