using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class removeColunsMedicalConsultation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_MedicalConsultation_doctorId",
                table: "MedicalConsultation");

            migrationBuilder.DropIndex(
                name: "IX_MedicalConsultation_idPrescription",
                table: "MedicalConsultation");

            migrationBuilder.DropIndex(
                name: "IX_MedicalConsultation_patientId",
                table: "MedicalConsultation");

            migrationBuilder.DropIndex(
                name: "IX_MedicalConsultation_typeAppointmentMedical",
                table: "MedicalConsultation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_doctorId",
                table: "MedicalConsultation",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_idPrescription",
                table: "MedicalConsultation",
                column: "idPrescription");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_patientId",
                table: "MedicalConsultation",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_typeAppointmentMedical",
                table: "MedicalConsultation",
                column: "typeAppointmentMedical");

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
    }
}
