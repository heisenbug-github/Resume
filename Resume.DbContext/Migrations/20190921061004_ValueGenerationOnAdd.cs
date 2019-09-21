using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.DbContext.Migrations
{
    public partial class ValueGenerationOnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "message",
                newName: "sender_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sender_name",
                table: "message",
                newName: "name");
        }
    }
}
