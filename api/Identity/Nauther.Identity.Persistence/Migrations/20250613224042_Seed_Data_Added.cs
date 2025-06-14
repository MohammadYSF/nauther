using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nauther.Identity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Data_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2379b9d4-ca76-467b-bea7-6be08bcef55c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2479b9d4-ca76-467b-bea7-6be08bcef55c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2579b9d4-ca76-467b-bea7-6be08bcef55c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("67b1e085-d1ff-496a-9adb-5c57a2c0bb3d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[,]
                {
                    { new Guid("0010928a-3381-46aa-b9b7-c2db83dd0f82"), "حذف نقش", "DeleteRole" },
                    { new Guid("093d581c-28bb-4700-83a2-429d21765ee6"), "مشاهده دسترسی", "ViewPermission" },
                    { new Guid("0f5c4756-d479-4029-a1c5-165f7385accb"), "ایجاد نقش", "AddRole" },
                    { new Guid("3180f21d-0785-4293-a8fe-281a533b768c"), "حذف ادمین", "DeleteAdmin" },
                    { new Guid("62ec4f35-44ca-4c63-82b9-689d2bfde0c6"), "ویرایش نقش", "EditRole" },
                    { new Guid("7c52899e-c7b1-4074-95fc-4257120b5b29"), "ویرایش دسترسی", "EditPermission" },
                    { new Guid("8ca6c12d-a066-4859-a3a0-f786de32821c"), "ویرایش ادمین", "EditAdmin" },
                    { new Guid("9a5a8465-cb5b-44eb-bbe5-16a463a03c37"), "مشاهده ادمین", "ViewAdmin" },
                    { new Guid("9a755e5d-4380-49ab-bae2-06480b8e9476"), "مشاهده نقش", "ViewRole" },
                    { new Guid("a40cda85-a303-46d7-8350-cbed507efb12"), "ایجاد دسترسی", "AddPermission" },
                    { new Guid("be05bebe-21e7-4d7f-964f-ce4030373ac6"), "حذف دسترسی", "DeletePermission" },
                    { new Guid("c1a243db-a5d5-4d5c-bae4-02a8ccf5fd33"), "ایجاد ادمین", "AddAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[] { new Guid("081e66fc-527d-47a7-941f-7af16e95e738"), "نقش ادمین", "Admin" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("0010928a-3381-46aa-b9b7-c2db83dd0f82"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("093d581c-28bb-4700-83a2-429d21765ee6"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("0f5c4756-d479-4029-a1c5-165f7385accb"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("3180f21d-0785-4293-a8fe-281a533b768c"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("62ec4f35-44ca-4c63-82b9-689d2bfde0c6"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("7c52899e-c7b1-4074-95fc-4257120b5b29"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("8ca6c12d-a066-4859-a3a0-f786de32821c"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("9a5a8465-cb5b-44eb-bbe5-16a463a03c37"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("9a755e5d-4380-49ab-bae2-06480b8e9476"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("a40cda85-a303-46d7-8350-cbed507efb12"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("be05bebe-21e7-4d7f-964f-ce4030373ac6"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") },
                    { new Guid("c1a243db-a5d5-4d5c-bae4-02a8ccf5fd33"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0010928a-3381-46aa-b9b7-c2db83dd0f82"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("093d581c-28bb-4700-83a2-429d21765ee6"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("0f5c4756-d479-4029-a1c5-165f7385accb"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("3180f21d-0785-4293-a8fe-281a533b768c"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("62ec4f35-44ca-4c63-82b9-689d2bfde0c6"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("7c52899e-c7b1-4074-95fc-4257120b5b29"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("8ca6c12d-a066-4859-a3a0-f786de32821c"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("9a5a8465-cb5b-44eb-bbe5-16a463a03c37"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("9a755e5d-4380-49ab-bae2-06480b8e9476"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("a40cda85-a303-46d7-8350-cbed507efb12"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("be05bebe-21e7-4d7f-964f-ce4030373ac6"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { new Guid("c1a243db-a5d5-4d5c-bae4-02a8ccf5fd33"), new Guid("081e66fc-527d-47a7-941f-7af16e95e738") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0010928a-3381-46aa-b9b7-c2db83dd0f82"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("093d581c-28bb-4700-83a2-429d21765ee6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0f5c4756-d479-4029-a1c5-165f7385accb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("3180f21d-0785-4293-a8fe-281a533b768c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("62ec4f35-44ca-4c63-82b9-689d2bfde0c6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7c52899e-c7b1-4074-95fc-4257120b5b29"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8ca6c12d-a066-4859-a3a0-f786de32821c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9a5a8465-cb5b-44eb-bbe5-16a463a03c37"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9a755e5d-4380-49ab-bae2-06480b8e9476"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a40cda85-a303-46d7-8350-cbed507efb12"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("be05bebe-21e7-4d7f-964f-ce4030373ac6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c1a243db-a5d5-4d5c-bae4-02a8ccf5fd33"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("081e66fc-527d-47a7-941f-7af16e95e738"));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[,]
                {
                    { new Guid("2379b9d4-ca76-467b-bea7-6be08bcef55c"), "ایجاد دسترسی", "CreatePermission" },
                    { new Guid("2479b9d4-ca76-467b-bea7-6be08bcef55c"), "ویرایش دسترسی", "EditPermission" },
                    { new Guid("2579b9d4-ca76-467b-bea7-6be08bcef55c"), "حذف دسترسی", "DeletePermission" },
                    { new Guid("67b1e085-d1ff-496a-9adb-5c57a2c0bb3d"), "مشاهده دسترسی ها", "ViewPermission" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[,]
                {
                    { new Guid("717d7a4a-d864-4774-8ae0-5010e745d87e"), "کاربر", "User" },
                    { new Guid("bd7a5b0b-3059-4fab-8c36-b385b8baa9c9"), "ادمین", "Admin" }
                });
        }
    }
}
