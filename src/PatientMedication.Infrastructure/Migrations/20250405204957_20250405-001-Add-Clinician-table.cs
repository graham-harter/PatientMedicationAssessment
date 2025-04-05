using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMedication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _20250405001AddCliniciantable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "clinicians");

            migrationBuilder.CreateSequence(
                name: "NextClinicianReference",
                schema: "clinicians",
                startValue: 100000000001L,
                minValue: 100000000001L,
                maxValue: 999999999999L);

            migrationBuilder.CreateTable(
                name: "Clinician",
                schema: "clinicians",
                columns: table => new
                {
                    ClinicianId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicianReference = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegistrationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinician", x => x.ClinicianId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clinician",
                schema: "clinicians");

            migrationBuilder.DropSequence(
                name: "NextClinicianReference",
                schema: "clinicians");
        }
    }
}
