using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapicore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medico",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apellidopat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apellidomat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    esespecialista = table.Column<bool>(type: "bit", nullable: false),
                    habilitado = table.Column<bool>(type: "bit", nullable: false),
                    borrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medico", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    borrado = table.Column<bool>(type: "bit", nullable: false),
                    apellidopat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apellidomar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paciente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ingreso",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    numerosala = table.Column<int>(type: "int", nullable: false),
                    numerocama = table.Column<int>(type: "int", nullable: false),
                    diagnostico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    borrado = table.Column<bool>(type: "bit", nullable: false),
                    medicoid = table.Column<long>(type: "bigint", nullable: false),
                    pacienteid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingreso", x => x.id);
                    table.ForeignKey(
                        name: "FK_ingreso_medico_medicoid",
                        column: x => x.medicoid,
                        principalTable: "medico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ingreso_paciente_pacienteid",
                        column: x => x.pacienteid,
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "egreso",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tratamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    borrado = table.Column<bool>(type: "bit", nullable: false),
                    medicoid = table.Column<long>(type: "bigint", nullable: false),
                    ingresoid = table.Column<long>(type: "bigint", nullable: false),
                    pacienteid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_egreso", x => x.id);
                    table.ForeignKey(
                        name: "FK_egreso_ingreso_ingresoid",
                        column: x => x.ingresoid,
                        principalTable: "ingreso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_egreso_medico_medicoid",
                        column: x => x.medicoid,
                        principalTable: "medico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_egreso_paciente_pacienteid",
                        column: x => x.pacienteid,
                        principalTable: "paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_egreso_ingresoid",
                table: "egreso",
                column: "ingresoid");

            migrationBuilder.CreateIndex(
                name: "IX_egreso_medicoid",
                table: "egreso",
                column: "medicoid");

            migrationBuilder.CreateIndex(
                name: "IX_egreso_pacienteid",
                table: "egreso",
                column: "pacienteid");

            migrationBuilder.CreateIndex(
                name: "IX_ingreso_medicoid",
                table: "ingreso",
                column: "medicoid");

            migrationBuilder.CreateIndex(
                name: "IX_ingreso_pacienteid",
                table: "ingreso",
                column: "pacienteid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "egreso");

            migrationBuilder.DropTable(
                name: "ingreso");

            migrationBuilder.DropTable(
                name: "medico");

            migrationBuilder.DropTable(
                name: "paciente");
        }
    }
}
