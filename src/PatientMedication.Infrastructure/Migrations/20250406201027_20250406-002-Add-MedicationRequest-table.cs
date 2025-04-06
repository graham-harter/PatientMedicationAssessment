using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMedication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20250406002AddMedicationRequesttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Patient_PatientReference",
                schema: "patients",
                table: "Patient",
                column: "PatientReference");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Medication_Code",
                schema: "medication",
                table: "Medication",
                column: "Code");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Clinician_ClinicianReference",
                schema: "clinicians",
                table: "Clinician",
                column: "ClinicianReference");

            migrationBuilder.CreateTable(
                name: "MedicationRequest",
                schema: "medication",
                columns: table => new
                {
                    MedicationRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientReference = table.Column<long>(type: "bigint", nullable: false),
                    ClinicianReference = table.Column<long>(type: "bigint", nullable: false),
                    MedicationReference = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ReasonText = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    PrescribedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FrequencyNumberOfTimes = table.Column<int>(type: "int", nullable: false),
                    FrequencyUnitId = table.Column<int>(type: "int", nullable: false),
                    MedicationRequestStatusId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationRequest", x => x.MedicationRequestId);
                    table.ForeignKey(
                        name: "FK_MedicationRequest_Clinician_ClinicianReference",
                        column: x => x.ClinicianReference,
                        principalSchema: "clinicians",
                        principalTable: "Clinician",
                        principalColumn: "ClinicianReference",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationRequest_MedicationRequestStatus_MedicationRequestStatusId",
                        column: x => x.MedicationRequestStatusId,
                        principalSchema: "medication",
                        principalTable: "MedicationRequestStatus",
                        principalColumn: "MedicationRequestStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationRequest_Medication_MedicationReference",
                        column: x => x.MedicationReference,
                        principalSchema: "medication",
                        principalTable: "Medication",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationRequest_Patient_PatientReference",
                        column: x => x.PatientReference,
                        principalSchema: "patients",
                        principalTable: "Patient",
                        principalColumn: "PatientReference",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationRequest_ClinicianReference",
                schema: "medication",
                table: "MedicationRequest",
                column: "ClinicianReference");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationRequest_MedicationReference",
                schema: "medication",
                table: "MedicationRequest",
                column: "MedicationReference");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationRequest_MedicationRequestStatusId",
                schema: "medication",
                table: "MedicationRequest",
                column: "MedicationRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationRequest_PatientReference",
                schema: "medication",
                table: "MedicationRequest",
                column: "PatientReference");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationRequest",
                schema: "medication");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Patient_PatientReference",
                schema: "patients",
                table: "Patient");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Medication_Code",
                schema: "medication",
                table: "Medication");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Clinician_ClinicianReference",
                schema: "clinicians",
                table: "Clinician");
        }
    }
}
