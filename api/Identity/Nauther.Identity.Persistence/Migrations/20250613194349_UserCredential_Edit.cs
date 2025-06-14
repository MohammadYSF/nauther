using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nauther.Identity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserCredential_Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserCredentials");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserCredentials");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserCredentials");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserCredentials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserCredentials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "UserCredentials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserCredentials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "UserCredentials",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
