using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogStore.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_lastupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Isvalid",
                table: "Comments",
                newName: "IsValid");

            migrationBuilder.AddColumn<bool>(
                name: "IsToxic",
                table: "Comments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToxicityAnalyzedDate",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToxicityCategory",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToxicityReason",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ToxicityScore",
                table: "Comments",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryDescription",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsToxic",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ToxicityAnalyzedDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ToxicityCategory",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ToxicityReason",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ToxicityScore",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CategoryDescription",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "Comments",
                newName: "Isvalid");
        }
    }
}
