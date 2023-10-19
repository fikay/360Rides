using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _360.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "services",
                columns: new[] { "Id", "ServiceDescription", "ServiceName", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, "Schedule dates for pickup for your children and we will be there to pick them Up", "Child Pickup", "Fikayo", new DateTime(2023, 10, 18, 4, 26, 4, 808, DateTimeKind.Local).AddTicks(8633) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "services");
        }
    }
}
