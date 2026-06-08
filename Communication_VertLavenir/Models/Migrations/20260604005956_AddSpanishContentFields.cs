using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class AddSpanishContentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionEs",
                table: "OrgValues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEs",
                table: "OrgValues",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEs",
                table: "KeyActions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEs",
                table: "KeyActions",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelEs",
                table: "ImpactMetrics",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEs",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEs",
                table: "Events",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEs",
                table: "OrgValues");

            migrationBuilder.DropColumn(
                name: "TitleEs",
                table: "OrgValues");

            migrationBuilder.DropColumn(
                name: "DescriptionEs",
                table: "KeyActions");

            migrationBuilder.DropColumn(
                name: "TitleEs",
                table: "KeyActions");

            migrationBuilder.DropColumn(
                name: "LabelEs",
                table: "ImpactMetrics");

            migrationBuilder.DropColumn(
                name: "DescriptionEs",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TitleEs",
                table: "Events");
        }
    }
}
