using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KafeAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedAddresspropForCafeInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CafeInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CafeInfos");
        }
    }
}
