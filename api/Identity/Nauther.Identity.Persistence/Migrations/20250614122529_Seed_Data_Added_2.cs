using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nauther.Identity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Data_Added_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                column: "Id",
                value: "65f84f5e-38d6-410a-a9a1-7e1cdff64b33");

            migrationBuilder.InsertData(
                table: "UserCredentials",
                columns: new[] { "UserId", "PasswordHash" },
                values: new object[] { "65f84f5e-38d6-410a-a9a1-7e1cdff64b33", "$argon2id$v=19$m=65536,t=3,p=1$Ep84MCTbcLcNvLawUW0yRw$pO29hahtMewUOFS4WXMwL0u7LL5SXNMXe2fUI3RoKlg" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("081e66fc-527d-47a7-941f-7af16e95e738"), "65f84f5e-38d6-410a-a9a1-7e1cdff64b33" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserCredentials",
                keyColumn: "UserId",
                keyValue: "65f84f5e-38d6-410a-a9a1-7e1cdff64b33");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("081e66fc-527d-47a7-941f-7af16e95e738"), "65f84f5e-38d6-410a-a9a1-7e1cdff64b33" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "65f84f5e-38d6-410a-a9a1-7e1cdff64b33");
        }
    }
}
