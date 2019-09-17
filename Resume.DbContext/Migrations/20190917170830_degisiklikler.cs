using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.DbContext.Migrations
{
    public partial class degisiklikler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_message_clustered_index",
                table: "message",
                column: "clustered_index",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_message_clustered_index",
                table: "message");
        }
    }
}
