using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsWebExample.Migrations
{
    public partial class AddPictureToTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Tags",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Tags");
        }
    }
}
