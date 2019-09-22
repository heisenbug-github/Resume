using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Resume.DbContext.Migrations
{
    public partial class LogAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    clustered_index = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    user_name = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "log",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    clustered_index = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    record_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: true),
                    change_type = table.Column<string>(nullable: false),
                    change_date = table.Column<DateTime>(nullable: false),
                    entity_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log", x => x.id);
                    table.ForeignKey(
                        name: "fk_log_users_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "log_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    clustered_index = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    log_id = table.Column<Guid>(nullable: false),
                    property_name = table.Column<string>(nullable: false),
                    original_value = table.Column<string>(nullable: false),
                    new_value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log_detail", x => x.id);
                    table.ForeignKey(
                        name: "fk_log_detail_log_log_id",
                        column: x => x.log_id,
                        principalTable: "log",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_log_clustered_index",
                table: "log",
                column: "clustered_index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_log_user_id",
                table: "log",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_log_detail_clustered_index",
                table: "log_detail",
                column: "clustered_index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_log_detail_log_id",
                table: "log_detail",
                column: "log_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_clustered_index",
                table: "user",
                column: "clustered_index",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "log_detail");

            migrationBuilder.DropTable(
                name: "log");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
