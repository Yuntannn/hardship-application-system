using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HardshipApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixApplicantTimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Applicants",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Applicants",
                newName: "CreateAt");
        }
    }
}
