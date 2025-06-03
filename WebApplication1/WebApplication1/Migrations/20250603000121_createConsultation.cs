using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class createConsultation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeAppointmentMedical",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAppointmentMedical", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "TypeAppointmentMedical",
                columns: new[] { "description", "value" },
                values: new object[,]
                {
                    { "Consulta clínica geral", 100.0 },
                    { "Consulta especializada", 200.0 },
                    { "Retorno", 50.0 }
                });

            migrationBuilder.CreateTable(
                name: "MedicalConsultation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typeAppointmentMedical = table.Column<int>(type: "integer", nullable: false),
                    doctorId = table.Column<int>(type: "integer", nullable: false),
                    patientId = table.Column<int>(type: "integer", nullable: false),
                    consultationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    consultationLink = table.Column<string>(type: "text", nullable: false),
                    agreementDiscount = table.Column<double>(type: "double precision", nullable: false),
                    justificationUpdate = table.Column<string>(type: "text", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalConsultation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalConsultation_TypeAppointmentMedical_typeAppointmentMedical",
                        column: x => x.typeAppointmentMedical,
                        principalTable: "TypeAppointmentMedical",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalConsultation_User_doctorId",
                        column: x => x.doctorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalConsultation_User_patientId",
                        column: x => x.patientId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_typeAppointmentMedical",
                table: "MedicalConsultation",
                column: "typeAppointmentMedical");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_doctorId",
                table: "MedicalConsultation",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_patientId",
                table: "MedicalConsultation",
                column: "patientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalConsultation");

            migrationBuilder.DropTable(
                name: "TypeAppointmentMedical");
        }
    }
}
