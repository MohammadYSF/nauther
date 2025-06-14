using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nauther.Identity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Data_Added_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserCredentials",
                keyColumn: "UserId",
                keyValue: "65f84f5e-38d6-410a-a9a1-7e1cdff64b33",
                column: "PasswordHash",
                value: "$argon2id$v=19$m=65536,t=3,p=1$vCyJ1Bp2Vbb2Yh0LVIhh+w$LXPvVkoFs0bqF3OmXb4hWUpPlBvjouUi2bw/S0q0oEc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserCredentials",
                keyColumn: "UserId",
                keyValue: "65f84f5e-38d6-410a-a9a1-7e1cdff64b33",
                column: "PasswordHash",
                value: "$argon2id$v=19$m=65536,t=3,p=1$Ep84MCTbcLcNvLawUW0yRw$pO29hahtMewUOFS4WXMwL0u7LL5SXNMXe2fUI3RoKlg");
        }
    }
}
