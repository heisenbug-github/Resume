using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.DbContext.Migrations
{
    public partial class AddedVisitId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "visit_id",
                table: "log",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_log_visit_id",
                table: "log",
                column: "visit_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_log_visit_id",
                table: "log");

            migrationBuilder.DropColumn(
                name: "visit_id",
                table: "log");
        }
    }
}
