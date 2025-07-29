using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteOrdering.Migrations
{
    /// <inheritdoc />
    public partial class CreateMonAnGhepStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MONANGHEPSTATS",
                columns: table => new
                {
                    IDMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    SOLANDUOCGHEP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MONANGHEPSTATS", x => x.IDMONAN);
                    table.ForeignKey(
                        name: "FK_MONANGHEPSTATS_MONAN",
                        column: x => x.IDMONAN,
                        principalTable: "MONAN",
                        principalColumn: "IDMONAN",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MONANGHEPSTATS");
        }
    }
}
