using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nauther.Identity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeIsActiveDefaultVakueToFalse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("1fd525dd-6d5e-4f60-99a0-ea96126f3560"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2875fc32-4198-4db3-bcb5-9218a7482487"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2ec312e7-4ed7-4eac-a853-9398f8bfe87c"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("49b8b2e8-403c-4d13-9b36-77265c27da67"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("59e3036e-2f1b-4888-8c88-4dfd7b4274fe"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("663b4a3f-5256-4cb3-82c3-bb232ffca641"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6e8db7f5-61fa-49ff-ac60-d5290b0091fa"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("72fa6089-a915-4730-a1ea-b6207f50ca38"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("7b3b02b6-3521-4b5b-86c2-002d72241140"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("85613123-a871-42cb-b580-c599954445c1"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("87392d3b-27c5-46a8-8651-8bfcb7410189"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b3084e6b-245b-4cf5-9393-f48964ee651e"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("bad5ede7-497e-45a2-a73e-bca401d9f31a"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("bea47f9a-45a8-4c64-b5d5-b86227968bb7"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c2f0e1f7-f514-4702-aa58-c1fddfbb0e28"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cbed817d-891c-481d-85ad-b250a50ebba1"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d41b0ffa-319c-4641-ac2d-2cc6fea9a12c"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d875b7c9-3eda-4007-a9f5-956450a2ee02"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e8ec273a-e412-4739-8209-3c34ae136d60"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f085c7a7-4802-4f42-8220-cde9b2f52c7a"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f87811d3-958b-49fe-ab4c-ac911ad37447"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("fadce983-829a-4900-a77c-c8d68cc22923"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("1fd525dd-6d5e-4f60-99a0-ea96126f3560"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2379b9d4-ca76-467b-bea7-6be08bcef55c"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2875fc32-4198-4db3-bcb5-9218a7482487"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("2ec312e7-4ed7-4eac-a853-9398f8bfe87c"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("43da1a86-2c5d-414b-878f-a2da33e06922"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("49b8b2e8-403c-4d13-9b36-77265c27da67"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4ebc2c2d-b57a-4714-a267-337c00cfbf0c"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("4ff76e7f-7080-4b45-b2bc-9331559bb5fa"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("541cc258-a23e-42a4-b681-3240bd44565b"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("59e3036e-2f1b-4888-8c88-4dfd7b4274fe"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("663b4a3f-5256-4cb3-82c3-bb232ffca641"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("67b1e085-d1ff-496a-9adb-5c57a2c0bb3d"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6e8db7f5-61fa-49ff-ac60-d5290b0091fa"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("6fcac8c9-c0e0-45e8-bedf-07a758a9d86a"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("72fa6089-a915-4730-a1ea-b6207f50ca38"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("73fa46db-649f-463a-9380-a98f880b7d5e"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("7b3b02b6-3521-4b5b-86c2-002d72241140"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("85613123-a871-42cb-b580-c599954445c1"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("87392d3b-27c5-46a8-8651-8bfcb7410189"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("88468f14-c320-42f1-a95d-58edf5f3bb01"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("97fafd05-5297-4d28-b3bf-8cfe3cb6e5e1"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a6c055f2-9c85-44fb-bec1-0ec1abd552c6"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("aaf3762f-2271-4cf4-96f5-8d5eaf390a72"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("ab7e9cad-3089-4c83-aa7d-7c4cc35bd462"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("b3084e6b-245b-4cf5-9393-f48964ee651e"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("baca0f7b-7837-4b33-9052-3fc64532b294"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("bad5ede7-497e-45a2-a73e-bca401d9f31a"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("bea47f9a-45a8-4c64-b5d5-b86227968bb7"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c1e5f8f3-37c1-495d-9968-4bdab20b6467"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c2f0e1f7-f514-4702-aa58-c1fddfbb0e28"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c37c06d8-6139-4e7c-9a9c-9aee1f2f1e42"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cbed817d-891c-481d-85ad-b250a50ebba1"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("cc3bef25-a9a5-461b-9f54-2d882570456f"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d41b0ffa-319c-4641-ac2d-2cc6fea9a12c"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d7b02687-990f-4d0d-8464-3a3ad2f70f93"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("d875b7c9-3eda-4007-a9f5-956450a2ee02"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("dc9a9190-fb09-4ecf-8205-80f18ac14906"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("e8ec273a-e412-4739-8209-3c34ae136d60"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f085c7a7-4802-4f42-8220-cde9b2f52c7a"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f87811d3-958b-49fe-ab4c-ac911ad37447"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("f91b2819-eb5a-4845-99ed-4d281dce5414"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("fadce983-829a-4900-a77c-c8d68cc22923"), new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1fd525dd-6d5e-4f60-99a0-ea96126f3560"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2379b9d4-ca76-467b-bea7-6be08bcef55c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2875fc32-4198-4db3-bcb5-9218a7482487"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2ec312e7-4ed7-4eac-a853-9398f8bfe87c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("43da1a86-2c5d-414b-878f-a2da33e06922"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("49b8b2e8-403c-4d13-9b36-77265c27da67"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4ebc2c2d-b57a-4714-a267-337c00cfbf0c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4ff76e7f-7080-4b45-b2bc-9331559bb5fa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("541cc258-a23e-42a4-b681-3240bd44565b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("59e3036e-2f1b-4888-8c88-4dfd7b4274fe"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("663b4a3f-5256-4cb3-82c3-bb232ffca641"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("67b1e085-d1ff-496a-9adb-5c57a2c0bb3d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6e8db7f5-61fa-49ff-ac60-d5290b0091fa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("72fa6089-a915-4730-a1ea-b6207f50ca38"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("73fa46db-649f-463a-9380-a98f880b7d5e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7b3b02b6-3521-4b5b-86c2-002d72241140"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("85613123-a871-42cb-b580-c599954445c1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("87392d3b-27c5-46a8-8651-8bfcb7410189"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("88468f14-c320-42f1-a95d-58edf5f3bb01"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("97fafd05-5297-4d28-b3bf-8cfe3cb6e5e1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aaf3762f-2271-4cf4-96f5-8d5eaf390a72"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ab7e9cad-3089-4c83-aa7d-7c4cc35bd462"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b3084e6b-245b-4cf5-9393-f48964ee651e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("baca0f7b-7837-4b33-9052-3fc64532b294"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bad5ede7-497e-45a2-a73e-bca401d9f31a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bea47f9a-45a8-4c64-b5d5-b86227968bb7"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c1e5f8f3-37c1-495d-9968-4bdab20b6467"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c2f0e1f7-f514-4702-aa58-c1fddfbb0e28"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c37c06d8-6139-4e7c-9a9c-9aee1f2f1e42"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cbed817d-891c-481d-85ad-b250a50ebba1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d41b0ffa-319c-4641-ac2d-2cc6fea9a12c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d7b02687-990f-4d0d-8464-3a3ad2f70f93"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d875b7c9-3eda-4007-a9f5-956450a2ee02"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("dc9a9190-fb09-4ecf-8205-80f18ac14906"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e8ec273a-e412-4739-8209-3c34ae136d60"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f085c7a7-4802-4f42-8220-cde9b2f52c7a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f87811d3-958b-49fe-ab4c-ac911ad37447"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f91b2819-eb5a-4845-99ed-4d281dce5414"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("fadce983-829a-4900-a77c-c8d68cc22923"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("72fa6089-a915-4730-a1ea-b6207f50ca38"), "GetActivityLicensesByCompanyId" },
                    { new Guid("73fa46db-649f-463a-9380-a98f880b7d5e"), "CreateUserRole" },
                    { new Guid("7b3b02b6-3521-4b5b-86c2-002d72241140"), "GetFieldOfActivitiesByCompanyId" },
                    { new Guid("85613123-a871-42cb-b580-c599954445c1"), "AddFieldOfActivity" },
                    { new Guid("87392d3b-27c5-46a8-8651-8bfcb7410189"), "GetPPEByCompanyId" },
                    { new Guid("88468f14-c320-42f1-a95d-58edf5f3bb01"), "GetPermissionByName" },
                    { new Guid("97fafd05-5297-4d28-b3bf-8cfe3cb6e5e1"), "GetRoleById" },
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
                values: new object[,]
                {
                    { new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), "User" },
                    { new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "CreatedAt", "CreatedBy", "Id", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("1fd525dd-6d5e-4f60-99a0-ea96126f3560"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("5a5f53f1-a745-430f-8e78-93ce9d46acb0"), null, null },
                    { new Guid("2875fc32-4198-4db3-bcb5-9218a7482487"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("4fe4bc7e-42f1-4d83-8529-1394db0d595d"), null, null },
                    { new Guid("2ec312e7-4ed7-4eac-a853-9398f8bfe87c"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("a6a7a5c0-9e9f-4e2e-ae0e-12fbd87c73e1"), null, null },
                    { new Guid("49b8b2e8-403c-4d13-9b36-77265c27da67"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("70fb4427-ae1f-4ae9-a8fe-5d4f7b6602a6"), null, null },
                    { new Guid("59e3036e-2f1b-4888-8c88-4dfd7b4274fe"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("9f9e75dc-1a1c-4973-80f4-98b0d5bb6a47"), null, null },
                    { new Guid("663b4a3f-5256-4cb3-82c3-bb232ffca641"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("d5e5b72c-6a9d-4706-a2f1-d3ae7675a125"), null, null },
                    { new Guid("6e8db7f5-61fa-49ff-ac60-d5290b0091fa"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("9a5608c5-73f9-48bb-90c5-c1db19f5b727"), null, null },
                    { new Guid("72fa6089-a915-4730-a1ea-b6207f50ca38"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("22b74c51-ec3f-42e2-8c52-83da4046e59a"), null, null },
                    { new Guid("7b3b02b6-3521-4b5b-86c2-002d72241140"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("54d2d948-6bc3-43c4-9a3e-b52723a592ff"), null, null },
                    { new Guid("85613123-a871-42cb-b580-c599954445c1"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f38364fc-5f5a-463f-a72e-12e79b469a04"), null, null },
                    { new Guid("87392d3b-27c5-46a8-8651-8bfcb7410189"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("33b1f640-5e8f-4a4a-8d63-2daf34823067"), null, null },
                    { new Guid("b3084e6b-245b-4cf5-9393-f48964ee651e"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("de44ed72-6a82-441b-a8a1-af5f3d37275c"), null, null },
                    { new Guid("bad5ede7-497e-45a2-a73e-bca401d9f31a"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f1e9e4c7-ca13-46b1-9a28-69ee625e424f"), null, null },
                    { new Guid("bea47f9a-45a8-4c64-b5d5-b86227968bb7"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("b5071c99-c0e2-41e8-b556-70d9d0e8f770"), null, null },
                    { new Guid("c2f0e1f7-f514-4702-aa58-c1fddfbb0e28"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f2a46b51-0212-4c7c-b7d7-b29d3c96a059"), null, null },
                    { new Guid("cbed817d-891c-481d-85ad-b250a50ebba1"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("c4d79018-e154-4c25-9188-98eca1d60e9e"), null, null },
                    { new Guid("d41b0ffa-319c-4641-ac2d-2cc6fea9a12c"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("aee4f144-6aa3-4186-9a68-a381fe05dca9"), null, null },
                    { new Guid("d875b7c9-3eda-4007-a9f5-956450a2ee02"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("bf6c67f1-9273-45a5-b292-871c81b1c9f4"), null, null },
                    { new Guid("e8ec273a-e412-4739-8209-3c34ae136d60"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("1738995e-4a7a-4164-b2a9-ff3a4da7d1f0"), null, null },
                    { new Guid("f085c7a7-4802-4f42-8220-cde9b2f52c7a"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("67e8c87a-32b4-4d35-a1a7-fc4d58f44d2f"), null, null },
                    { new Guid("f87811d3-958b-49fe-ab4c-ac911ad37447"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("8b5d28a7-32ae-41d3-9f03-51e7b2545c7b"), null, null },
                    { new Guid("fadce983-829a-4900-a77c-c8d68cc22923"), new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), new DateTime(2025, 4, 19, 15, 0, 56, 36, DateTimeKind.Unspecified).AddTicks(3387), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ee289c0f-20a9-4b9f-9e2a-3795700d8a6e"), null, null },
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
        }
    }
}
