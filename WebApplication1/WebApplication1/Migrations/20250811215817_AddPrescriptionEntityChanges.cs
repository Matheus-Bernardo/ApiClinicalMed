using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescriptionEntityChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idPrescription",
                table: "MedicalConsultation",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patientName = table.Column<string>(type: "text", nullable: false),
                    doctorName = table.Column<string>(type: "text", nullable: false),
                    validityPrescription = table.Column<int>(type: "integer", nullable: false),
                    crmDoctor = table.Column<int>(type: "integer", nullable: false),
                    remedyPrescription = table.Column<List<string>>(type: "text[]", nullable: false),
                    frequency = table.Column<string>(type: "text", nullable: false),
                    dosageRemedy = table.Column<string>(type: "text", nullable: false),
                    frequencyRemedy = table.Column<string>(type: "text", nullable: false),
                    observation = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_idPrescription",
                table: "MedicalConsultation",
                column: "idPrescription");
            

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalConsultation_Doctor_doctorId",
                table: "MedicalConsultation",
                column: "doctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalConsultation_Patient_patientId",
                table: "MedicalConsultation",
                column: "patientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalConsultation_Prescription_idPrescription",
                table: "MedicalConsultation",
                column: "idPrescription",
                principalTable: "Prescription",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalConsultation_TypeAppointmentMedical_typeAppointmentM~",
                table: "MedicalConsultation",
                column: "typeAppointmentMedical",
                principalTable: "TypeAppointmentMedical",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalConsultation_Doctor_doctorId",
                table: "MedicalConsultation");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalConsultation_Patient_patientId",
                table: "MedicalConsultation");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalConsultation_Prescription_idPrescription",
                table: "MedicalConsultation");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalConsultation_TypeAppointmentMedical_typeAppointmentM~",
                table: "MedicalConsultation");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_MedicalConsultation_doctorId",
                table: "MedicalConsultation");

            migrationBuilder.DropIndex(
                name: "IX_MedicalConsultation_idPrescription",
                table: "MedicalConsultation");

            migrationBuilder.DropIndex(
                name: "IX_MedicalConsultation_patientId",
                table: "MedicalConsultation");

            migrationBuilder.DropColumn(
                name: "idPrescription",
                table: "MedicalConsultation");
        }
    }
}
