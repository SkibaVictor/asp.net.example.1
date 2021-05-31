using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsWebExample.Migrations
{
    public partial class AttachmentAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "News",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    ModifyDateTime = table.Column<DateTime>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_AttachmentId",
                table: "News",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Attachments_AttachmentId",
                table: "News",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Attachments_AttachmentId",
                table: "News");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_News_AttachmentId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "News");
        }
    }
}
