using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class V1_0_Sync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SoftwarePackages_Name_Version",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "DetectionRule",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "InstallerType",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "RequiresRestart",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "SHA256",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "SilentInstallCommand",
                table: "SoftwarePackages",
                newName: "SetupFileUrl");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Orders",
                newName: "CustomerGovernorate");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "CustomerAddress");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "SoftwarePackages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "SoftwarePackages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Orders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerWhatsApp",
                table: "Orders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeBaseArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProblemName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    ProblemImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    YouTubeVideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Visible = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeBaseArticles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SoftwarePackages_Name",
                table: "SoftwarePackages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_IsActive",
                table: "CompanyProfiles",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBaseArticles_DisplayOrder",
                table: "KnowledgeBaseArticles",
                column: "DisplayOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyProfiles");

            migrationBuilder.DropTable(
                name: "KnowledgeBaseArticles");

            migrationBuilder.DropIndex(
                name: "IX_SoftwarePackages_Name",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "SoftwarePackages");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerWhatsApp",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "SetupFileUrl",
                table: "SoftwarePackages",
                newName: "SilentInstallCommand");

            migrationBuilder.RenameColumn(
                name: "CustomerGovernorate",
                table: "Orders",
                newName: "DeviceId");

            migrationBuilder.RenameColumn(
                name: "CustomerAddress",
                table: "Orders",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "SoftwarePackages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "DetectionRule",
                table: "SoftwarePackages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "SoftwarePackages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InstallerType",
                table: "SoftwarePackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresRestart",
                table: "SoftwarePackages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SHA256",
                table: "SoftwarePackages",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "SoftwarePackages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SoftwarePackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Orders",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwarePackages_Name_Version",
                table: "SoftwarePackages",
                columns: new[] { "Name", "Version" },
                unique: true);
        }
    }
}
