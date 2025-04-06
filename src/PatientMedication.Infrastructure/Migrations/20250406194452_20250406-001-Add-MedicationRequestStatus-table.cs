using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientMedication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20250406001AddMedicationRequestStatustable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicationRequestStatus",
                schema: "medication",
                columns: table => new
                {
                    MedicationRequestStatusId = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationRequestStatus", x => x.MedicationRequestStatusId);
                });

            migrationBuilder.InsertData(
                schema: "medication",
                table: "MedicationRequestStatus",
                columns: new[] { "MedicationRequestStatusId", "Description" },
                values: new object[,]
                {
                    { (short)1, "Active" },
                    { (short)2, "On hold" },
                    { (short)3, "Cancelled" },
                    { (short)4, "Completed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationRequestStatus",
                schema: "medication");
        }
    }
}
