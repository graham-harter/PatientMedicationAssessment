using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMedication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20250405002AddMedicationCodeSystemtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "medication");

            migrationBuilder.CreateTable(
                name: "MedicationCodeSystem",
                schema: "medication",
                columns: table => new
                {
                    MedicationCodeSystemId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationCodeSystem", x => x.MedicationCodeSystemId);
                });

            migrationBuilder.InsertData(
                schema: "medication",
                table: "MedicationCodeSystem",
                columns: new[] { "MedicationCodeSystemId", "Code" },
                values: new object[] { 1, "SNOMED" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationCodeSystem",
                schema: "medication");
        }
    }
}
