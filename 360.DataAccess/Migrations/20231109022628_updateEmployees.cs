using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _360.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 8, 20, 26, 26, 639, DateTimeKind.Local).AddTicks(2569), new DateTime(2023, 11, 8, 20, 26, 26, 639, DateTimeKind.Local).AddTicks(2557) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 4, 21, 28, 11, 600, DateTimeKind.Local).AddTicks(3629), new DateTime(2023, 11, 4, 21, 28, 11, 600, DateTimeKind.Local).AddTicks(2769) });
        }
    }
}
