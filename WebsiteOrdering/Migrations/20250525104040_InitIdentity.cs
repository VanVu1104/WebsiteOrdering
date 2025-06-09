using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteOrdering.Migrations
{
    /// <inheritdoc />
    public partial class InitIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOAIMONAN",
                columns: table => new
                {
                    IDLOAIMONAN = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TENLOAIMONAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOAIMONAN", x => x.IDLOAIMONAN);
                });

            migrationBuilder.CreateTable(
                name: "SIZE",
                columns: table => new
                {
                    SIZE = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TENSIZE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIZE", x => x.SIZE);
                });

            migrationBuilder.CreateTable(
                name: "MONAN",
                columns: table => new
                {
                    IDMONAN = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IDMONAN2 = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TENMONAN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MOTAMONAN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GIACOBAN = table.Column<int>(type: "int", nullable: false),
                    ANHMONAN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TRANGTHAI = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CategoryViewModelIDLOAIMONAN = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MONAN", x => new { x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK_MONAN_LOAIMONAN_CategoryViewModelIDLOAIMONAN",
                        column: x => x.CategoryViewModelIDLOAIMONAN,
                        principalTable: "LOAIMONAN",
                        principalColumn: "IDLOAIMONAN");
                });

            migrationBuilder.CreateTable(
                name: "LISTGIASIZE",
                columns: table => new
                {
                    IDLOAIMONAN = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IDSIZE = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    GIA = table.Column<int>(type: "int", nullable: false),
                    ProductsViewModelIDMONAN = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    ProductsViewModelIDMONAN2 = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LISTGIASIZE", x => new { x.IDLOAIMONAN, x.IDSIZE });
                    table.ForeignKey(
                        name: "FK_LISTGIASIZE_LOAIMONAN_IDLOAIMONAN",
                        column: x => x.IDLOAIMONAN,
                        principalTable: "LOAIMONAN",
                        principalColumn: "IDLOAIMONAN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LISTGIASIZE_MONAN_ProductsViewModelIDMONAN_ProductsViewModelIDMONAN2",
                        columns: x => new { x.ProductsViewModelIDMONAN, x.ProductsViewModelIDMONAN2 },
                        principalTable: "MONAN",
                        principalColumns: new[] { "IDMONAN", "IDMONAN2" });
                    table.ForeignKey(
                        name: "FK_LISTGIASIZE_SIZE_IDSIZE",
                        column: x => x.IDSIZE,
                        principalTable: "SIZE",
                        principalColumn: "SIZE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LISTGIASIZE_IDSIZE",
                table: "LISTGIASIZE",
                column: "IDSIZE");

            migrationBuilder.CreateIndex(
                name: "IX_LISTGIASIZE_ProductsViewModelIDMONAN_ProductsViewModelIDMONAN2",
                table: "LISTGIASIZE",
                columns: new[] { "ProductsViewModelIDMONAN", "ProductsViewModelIDMONAN2" });

            migrationBuilder.CreateIndex(
                name: "IX_MONAN_CategoryViewModelIDLOAIMONAN",
                table: "MONAN",
                column: "CategoryViewModelIDLOAIMONAN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LISTGIASIZE");

            migrationBuilder.DropTable(
                name: "MONAN");

            migrationBuilder.DropTable(
                name: "SIZE");

            migrationBuilder.DropTable(
                name: "LOAIMONAN");
        }
    }
}
