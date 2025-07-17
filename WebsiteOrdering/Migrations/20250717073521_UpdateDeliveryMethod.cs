using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteOrdering.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeliveryMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryMethod",
                table: "DONHANG",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryMethod",
                table: "DONHANG");
        }
    }
}
