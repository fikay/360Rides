using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _360.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cretedby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "UpdatedDate" },
                values: new object[] { "Fikayo", new DateTime(2023, 10, 18, 21, 46, 12, 970, DateTimeKind.Local).AddTicks(3527) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "services");

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedDate",
                value: new DateTime(2023, 10, 18, 20, 41, 9, 190, DateTimeKind.Local).AddTicks(3987));
        }
    }
}
