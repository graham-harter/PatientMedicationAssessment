using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMedication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20250405003AddMedicationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medication",
                schema: "medication",
                columns: table => new
                {
                    MedicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CodeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MedicationCodeSystemId = table.Column<int>(type: "int", nullable: false),
                    StrengthValue = table.Column<short>(type: "smallint", nullable: false),
                    StrengthUnitId = table.Column<int>(type: "int", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.MedicationId);
                    table.ForeignKey(
                        name: "FK_Medication_MedicationCodeSystem_MedicationCodeSystemId",
                        column: x => x.MedicationCodeSystemId,
                        principalSchema: "medication",
                        principalTable: "MedicationCodeSystem",
                        principalColumn: "MedicationCodeSystemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medication_MedicationCodeSystemId",
                schema: "medication",
                table: "Medication",
                column: "MedicationCodeSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medication",
                schema: "medication");
        }
    }
}
