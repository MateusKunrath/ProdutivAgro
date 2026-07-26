using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdutivAgro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationResponsibleUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResponsibleUserId",
                table: "Organizations",
                type: "uuid",
                nullable: true);
            
            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ResponsibleUserId",
                table: "Organizations",
                column: "ResponsibleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Users_ResponsibleUserId",
                table: "Organizations",
                column: "ResponsibleUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Users_ResponsibleUserId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_ResponsibleUserId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserId",
                table: "Organizations");
        }
    }
}
