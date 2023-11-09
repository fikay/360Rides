using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _360.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Employees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "driverId",
                table: "ReceivedRequestDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 4, 21, 28, 11, 600, DateTimeKind.Local).AddTicks(3629), new DateTime(2023, 11, 4, 21, 28, 11, 600, DateTimeKind.Local).AddTicks(2769) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropColumn(
                name: "driverId",
                table: "ReceivedRequestDetails");

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 27, 57, 528, DateTimeKind.Local).AddTicks(4895), new DateTime(2023, 11, 3, 6, 27, 57, 528, DateTimeKind.Local).AddTicks(4873) });
        }
    }
}
