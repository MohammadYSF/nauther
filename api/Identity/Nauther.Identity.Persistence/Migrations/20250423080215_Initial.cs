using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nauther.Identity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermissions",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissions", x => new { x.GroupId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_GroupPermissions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCredentials",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCredentials", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserCredentials_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1fd525dd-6d5e-4f60-99a0-ea96126f3560"), "GetAllCompanies" },
                    { new Guid("2379b9d4-ca76-467b-bea7-6be08bcef55c"), "CreatePermission" },
                    { new Guid("2875fc32-4198-4db3-bcb5-9218a7482487"), "GetWorkExperienceByCompanyId" },
                    { new Guid("2ec312e7-4ed7-4eac-a853-9398f8bfe87c"), "GetAllUsers" },
                    { new Guid("43da1a86-2c5d-414b-878f-a2da33e06922"), "CreateUserPermission" },
                    { new Guid("49b8b2e8-403c-4d13-9b36-77265c27da67"), "GetRnDByCompanyId" },
                    { new Guid("4ebc2c2d-b57a-4714-a267-337c00cfbf0c"), "GetGroupByName" },
                    { new Guid("4ff76e7f-7080-4b45-b2bc-9331559bb5fa"), "CreateRolePermission" },
                    { new Guid("541cc258-a23e-42a4-b681-3240bd44565b"), "GetAllGroups" },
                    { new Guid("59e3036e-2f1b-4888-8c88-4dfd7b4274fe"), "UpdateActivityLicense" },
                    { new Guid("663b4a3f-5256-4cb3-82c3-bb232ffca641"), "UpdateCompanyBaseInfo" },
                    { new Guid("67b1e085-d1ff-496a-9adb-5c57a2c0bb3d"), "GetPermissionById" },
                    { new Guid("6e8db7f5-61fa-49ff-ac60-d5290b0091fa"), "AddWorkExperience" },
                    { new Guid("6fcac8c9-c0e0-45e8-bedf-07a758a9d86a"), "RegisterUser" },
                    { new Guid("72fa6089-a915-4730-a1ea-b6207f50ca38"), "GetActivityLicensesByCompanyId" },
                    { new Guid("73fa46db-649f-463a-9380-a98f880b7d5e"), "CreateUserRole" },
                    { new Guid("7b3b02b6-3521-4b5b-86c2-002d72241140"), "GetFieldOfActivitiesByCompanyId" },
                    { new Guid("85613123-a871-42cb-b580-c599954445c1"), "AddFieldOfActivity" },
                    { new Guid("87392d3b-27c5-46a8-8651-8bfcb7410189"), "GetPPEByCompanyId" },
                    { new Guid("88468f14-c320-42f1-a95d-58edf5f3bb01"), "GetPermissionByName" },
                    { new Guid("97fafd05-5297-4d28-b3bf-8cfe3cb6e5e1"), "GetRoleById" },
                    { new Guid("a6c055f2-9c85-44fb-bec1-0ec1abd552c6"), "VerifyNationalId" },
                    { new Guid("aaf3762f-2271-4cf4-96f5-8d5eaf390a72"), "CreateRole" },
                    { new Guid("ab7e9cad-3089-4c83-aa7d-7c4cc35bd462"), "GetAllRoles" },
                    { new Guid("b3084e6b-245b-4cf5-9393-f48964ee651e"), "GetCompanyById" },
                    { new Guid("baca0f7b-7837-4b33-9052-3fc64532b294"), "CreateGroup" },
                    { new Guid("bad5ede7-497e-45a2-a73e-bca401d9f31a"), "GetCertificateByCompanyId" },
                    { new Guid("bea47f9a-45a8-4c64-b5d5-b86227968bb7"), "RegisterCompanyBaseInfo" },
                    { new Guid("c1e5f8f3-37c1-495d-9968-4bdab20b6467"), "CreateGroupPermission" },
                    { new Guid("c2f0e1f7-f514-4702-aa58-c1fddfbb0e28"), "AddCompanyHumanResource" },
                    { new Guid("c37c06d8-6139-4e7c-9a9c-9aee1f2f1e42"), "CreateUserGroup" },
                    { new Guid("cbed817d-891c-481d-85ad-b250a50ebba1"), "AddCertificate" },
                    { new Guid("cc3bef25-a9a5-461b-9f54-2d882570456f"), "LoginWithPassword" },
                    { new Guid("d41b0ffa-319c-4641-ac2d-2cc6fea9a12c"), "UpdateFieldOfActivity" },
                    { new Guid("d7b02687-990f-4d0d-8464-3a3ad2f70f93"), "GetGroupById" },
                    { new Guid("d875b7c9-3eda-4007-a9f5-956450a2ee02"), "GetUserDetail" },
                    { new Guid("dc9a9190-fb09-4ecf-8205-80f18ac14906"), "GetRoleByName" },
                    { new Guid("e8ec273a-e412-4739-8209-3c34ae136d60"), "GetCompanyHumanResourceByCompanyId" },
                    { new Guid("f085c7a7-4802-4f42-8220-cde9b2f52c7a"), "AddRnD" },
                    { new Guid("f87811d3-958b-49fe-ab4c-ac911ad37447"), "AddActivityLicense" },
                    { new Guid("f91b2819-eb5a-4845-99ed-4d281dce5414"), "GetAllPermissions" },
                    { new Guid("fadce983-829a-4900-a77c-c8d68cc22923"), "AddPPE" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), "Admin" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "CreatedAt", "CreatedBy", "Id", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1fd525dd-6d5e-4f60-99a0-ea96126f3560"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("61084070-1420-4c38-9c7d-3c560ad7ead0"), null, null },
                    { new Guid("2379b9d4-ca76-467b-bea7-6be08bcef55c"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("16e7a924-13d5-401d-a016-d2a2e1044c6f"), null, null },
                    { new Guid("2875fc32-4198-4db3-bcb5-9218a7482487"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("c1f483c4-f61a-48d7-ac41-ba78f6b1305b"), null, null },
                    { new Guid("2ec312e7-4ed7-4eac-a853-9398f8bfe87c"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("4ed2a8f7-a77b-488a-b7c8-ec5cec3c3e40"), null, null },
                    { new Guid("43da1a86-2c5d-414b-878f-a2da33e06922"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f3e5f1f9-7772-4c8b-ba4d-b5fc4634734a"), null, null },
                    { new Guid("49b8b2e8-403c-4d13-9b36-77265c27da67"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("61f91c35-030e-44ae-8456-a1128edc8dcf"), null, null },
                    { new Guid("4ebc2c2d-b57a-4714-a267-337c00cfbf0c"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("9eca2f8c-4d40-479e-ac09-fe1415bb27fe"), null, null },
                    { new Guid("4ff76e7f-7080-4b45-b2bc-9331559bb5fa"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("633f425c-e6c5-4dbb-a99f-dd15d4857d39"), null, null },
                    { new Guid("541cc258-a23e-42a4-b681-3240bd44565b"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("16eab8fe-7434-4222-adf2-4daa926d3b17"), null, null },
                    { new Guid("59e3036e-2f1b-4888-8c88-4dfd7b4274fe"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("b0d10c47-552c-4cbe-8fe8-3d81bf6839fb"), null, null },
                    { new Guid("663b4a3f-5256-4cb3-82c3-bb232ffca641"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("7a67e2ea-7a3d-4e7f-a9f8-fc98039915e4"), null, null },
                    { new Guid("67b1e085-d1ff-496a-9adb-5c57a2c0bb3d"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("095f218a-0348-459d-b2e0-75237a887df7"), null, null },
                    { new Guid("6e8db7f5-61fa-49ff-ac60-d5290b0091fa"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("b1bc7d34-0521-41c8-8032-b4312bab5f0c"), null, null },
                    { new Guid("6fcac8c9-c0e0-45e8-bedf-07a758a9d86a"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5148f413-281c-489d-8db6-e3401251a69c"), null, null },
                    { new Guid("72fa6089-a915-4730-a1ea-b6207f50ca38"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("854042be-cf8a-4284-b3b1-9f486435306b"), null, null },
                    { new Guid("73fa46db-649f-463a-9380-a98f880b7d5e"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("540813c8-38d4-4b74-b174-a2153c97abf9"), null, null },
                    { new Guid("7b3b02b6-3521-4b5b-86c2-002d72241140"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("a25939cc-4989-4073-8c32-d02e42dc15a9"), null, null },
                    { new Guid("85613123-a871-42cb-b580-c599954445c1"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("e9aa9eef-d00a-4861-9821-bc9de0b16fc7"), null, null },
                    { new Guid("87392d3b-27c5-46a8-8651-8bfcb7410189"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("900379df-31d4-46e3-b97f-92da77d8addb"), null, null },
                    { new Guid("88468f14-c320-42f1-a95d-58edf5f3bb01"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("19875c30-6ce3-4d4b-9d21-115b3ca4e4e7"), null, null },
                    { new Guid("97fafd05-5297-4d28-b3bf-8cfe3cb6e5e1"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("872c21a9-a168-4453-a5e2-06d8bcfd1556"), null, null },
                    { new Guid("a6c055f2-9c85-44fb-bec1-0ec1abd552c6"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("a9d36113-c8da-4701-8e03-8b554ccd8669"), null, null },
                    { new Guid("aaf3762f-2271-4cf4-96f5-8d5eaf390a72"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("2aa5c80d-a8b8-4246-b39b-8878b696d62c"), null, null },
                    { new Guid("ab7e9cad-3089-4c83-aa7d-7c4cc35bd462"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("76ea686a-1153-437f-8e32-a3cd6c1afbdc"), null, null },
                    { new Guid("b3084e6b-245b-4cf5-9393-f48964ee651e"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("c9c8aa97-9746-47bf-8e2a-23a8530bb425"), null, null },
                    { new Guid("baca0f7b-7837-4b33-9052-3fc64532b294"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("c912cada-f765-44ec-bbe4-bda8632f480f"), null, null },
                    { new Guid("bad5ede7-497e-45a2-a73e-bca401d9f31a"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("1969c968-167e-4cc5-8835-e045e49e67d7"), null, null },
                    { new Guid("bea47f9a-45a8-4c64-b5d5-b86227968bb7"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("e5e5f452-21d4-452a-a168-9da6e5ccd208"), null, null },
                    { new Guid("c1e5f8f3-37c1-495d-9968-4bdab20b6467"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f70cc5e7-b2ce-4354-babb-41a053fa6f45"), null, null },
                    { new Guid("c2f0e1f7-f514-4702-aa58-c1fddfbb0e28"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("cf73c899-d7c1-4a43-96fa-ccd4aa0e9858"), null, null },
                    { new Guid("c37c06d8-6139-4e7c-9a9c-9aee1f2f1e42"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f8cf6c6d-5ddd-4147-8244-e3a075d3c91f"), null, null },
                    { new Guid("cbed817d-891c-481d-85ad-b250a50ebba1"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("42f6cd33-f65a-48a0-a28e-d12a3d755cb6"), null, null },
                    { new Guid("cc3bef25-a9a5-461b-9f54-2d882570456f"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5bdbc803-d4cc-4f5d-8148-bd15910a287b"), null, null },
                    { new Guid("d41b0ffa-319c-4641-ac2d-2cc6fea9a12c"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("04fef867-7e00-4648-bd04-d368ff35124c"), null, null },
                    { new Guid("d7b02687-990f-4d0d-8464-3a3ad2f70f93"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f9710ca7-4154-463e-9b32-6db35f3351bf"), null, null },
                    { new Guid("d875b7c9-3eda-4007-a9f5-956450a2ee02"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("7a468d5c-31fe-4131-be61-8ce11e714d53"), null, null },
                    { new Guid("dc9a9190-fb09-4ecf-8205-80f18ac14906"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("adf36ba8-4940-4e00-9109-c924bee1ffbc"), null, null },
                    { new Guid("e8ec273a-e412-4739-8209-3c34ae136d60"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("65904dd7-5cdd-4f4e-8ada-c27656c565ab"), null, null },
                    { new Guid("f085c7a7-4802-4f42-8220-cde9b2f52c7a"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("28fdd323-17a4-4776-bced-e724922b62b4"), null, null },
                    { new Guid("f87811d3-958b-49fe-ab4c-ac911ad37447"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("66e0821e-14aa-40b3-b9b3-c9a7a82bf5e0"), null, null },
                    { new Guid("f91b2819-eb5a-4845-99ed-4d281dce5414"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("36972925-5d8c-4eb1-bce8-9ac1de8a7cf1"), null, null },
                    { new Guid("fadce983-829a-4900-a77c-c8d68cc22923"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5b985dfd-4ff0-4ee5-98fa-32636f31d44a"), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_PermissionId",
                table: "GroupPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupPermissions");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserCredentials");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
