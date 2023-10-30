using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _360.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class createHeaderandDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceivedRequestDetailsId",
                table: "serviceRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReceivedRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedRequestDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "receivedRequestHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    requestDetails = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receivedRequestHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_receivedRequestHeaders_AspNetUsers_Userid",
                        column: x => x.Userid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_receivedRequestHeaders_ReceivedRequestDetails_requestDetails",
                        column: x => x.requestDetails,
                        principalTable: "ReceivedRequestDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedDate",
                value: new DateTime(2023, 10, 28, 23, 57, 46, 392, DateTimeKind.Local).AddTicks(8575));

            migrationBuilder.CreateIndex(
                name: "IX_serviceRequests_ReceivedRequestDetailsId",
                table: "serviceRequests",
                column: "ReceivedRequestDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_receivedRequestHeaders_requestDetails",
                table: "receivedRequestHeaders",
                column: "requestDetails");

            migrationBuilder.CreateIndex(
                name: "IX_receivedRequestHeaders_Userid",
                table: "receivedRequestHeaders",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_serviceRequests_ReceivedRequestDetails_ReceivedRequestDetailsId",
                table: "serviceRequests",
                column: "ReceivedRequestDetailsId",
                principalTable: "ReceivedRequestDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_serviceRequests_ReceivedRequestDetails_ReceivedRequestDetailsId",
                table: "serviceRequests");

            migrationBuilder.DropTable(
                name: "receivedRequestHeaders");

            migrationBuilder.DropTable(
                name: "ReceivedRequestDetails");

            migrationBuilder.DropIndex(
                name: "IX_serviceRequests_ReceivedRequestDetailsId",
                table: "serviceRequests");

            migrationBuilder.DropColumn(
                name: "ReceivedRequestDetailsId",
                table: "serviceRequests");

            migrationBuilder.UpdateData(
                table: "services",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedDate",
                value: new DateTime(2023, 10, 24, 18, 48, 45, 297, DateTimeKind.Local).AddTicks(9947));
        }
    }
}
