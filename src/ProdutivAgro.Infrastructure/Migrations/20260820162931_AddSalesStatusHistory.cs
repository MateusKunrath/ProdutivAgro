using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdutivAgro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesStatusHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreviousStatus = table.Column<int>(type: "integer", nullable: false),
                    CurrentStatus = table.Column<int>(type: "integer", nullable: false),
                    ChangedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesStatusHistory_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesStatusHistory_Users_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesStatusHistory_ChangedByUserId",
                table: "SalesStatusHistory",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesStatusHistory_CurrentStatus",
                table: "SalesStatusHistory",
                column: "CurrentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SalesStatusHistory_PreviousStatus",
                table: "SalesStatusHistory",
                column: "PreviousStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SalesStatusHistory_SaleId",
                table: "SalesStatusHistory",
                column: "SaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesStatusHistory");
        }
    }
}
