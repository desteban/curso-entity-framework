using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace entity_framework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    categoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.categoriaId);
                });

            migrationBuilder.CreateTable(
                name: "tarea",
                columns: table => new
                {
                    tareaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    categoiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prioridad = table.Column<int>(type: "int", nullable: false),
                    creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tarea", x => x.tareaId);
                    table.ForeignKey(
                        name: "FK_tarea_categoria_categoiaId",
                        column: x => x.categoiaId,
                        principalTable: "categoria",
                        principalColumn: "categoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tarea_categoiaId",
                table: "tarea",
                column: "categoiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tarea");

            migrationBuilder.DropTable(
                name: "categoria");
        }
    }
}
