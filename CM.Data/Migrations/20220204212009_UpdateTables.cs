using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Limit",
                table: "Campaigns",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CampaignName",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalSalesCount",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Turnover",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "TotalAddedHour",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalAddedHour", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TotalAddedHour");

            migrationBuilder.DropColumn(
                name: "CampaignName",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "TotalSalesCount",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Turnover",
                table: "Campaigns");

            migrationBuilder.AlterColumn<int>(
                name: "Limit",
                table: "Campaigns",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
