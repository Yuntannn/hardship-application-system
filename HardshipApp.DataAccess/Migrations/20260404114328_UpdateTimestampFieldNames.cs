using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HardshipApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimestampFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "HardshipApplications",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "HardshipApplications",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "HardshipApplications",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "HardshipApplications",
                newName: "CreateAt");
        }
    }
}
