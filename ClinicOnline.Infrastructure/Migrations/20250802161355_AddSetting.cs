using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicOnline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Type = table.Column<int>(type: "integer", nullable: false, comment: "Loại cấu hình, ví dụ: Email = 1, SMS = 2..."),
                    Keys = table.Column<int>(type: "integer", nullable: false, comment: "Khóa cấu hình định danh, ví dụ: SMTP_PORT, ENABLE_NOTI"),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Tên hiển thị cấu hình"),
                    Value = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false, comment: "Giá trị cấu hình (string)"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false, comment: "Đánh dấu đã xóa mềm hay chưa"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdateBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Settings_pkey", x => x.Id);
                },
                comment: "Bảng lưu các cấu hình hệ thống");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
