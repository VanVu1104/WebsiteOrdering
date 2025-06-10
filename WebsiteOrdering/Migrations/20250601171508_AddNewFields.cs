using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteOrdering.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LISTGIASIZE_MONAN_ProductsViewModelIDMONAN_ProductsViewModelIDMONAN2",
                table: "LISTGIASIZE");

            migrationBuilder.DropForeignKey(
                name: "FK_MONAN_LOAIMONAN_CategoryViewModelIDLOAIMONAN",
                table: "MONAN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MONAN",
                table: "MONAN");

            migrationBuilder.RenameTable(
                name: "MONAN",
                newName: "SanPhams");

            migrationBuilder.RenameColumn(
                name: "SIZE",
                table: "SIZE",
                newName: "IDSIZE");

            migrationBuilder.RenameColumn(
                name: "CategoryViewModelIDLOAIMONAN",
                table: "SanPhams",
                newName: "CategoryIDLOAIMONAN");

            migrationBuilder.RenameIndex(
                name: "IX_MONAN_CategoryViewModelIDLOAIMONAN",
                table: "SanPhams",
                newName: "IX_SanPhams_CategoryIDLOAIMONAN");

            migrationBuilder.AddColumn<string>(
                name: "IDLOAIMONANCHA",
                table: "LOAIMONAN",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDLoaiMonAn",
                table: "SanPhams",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SanPhams",
                table: "SanPhams",
                columns: new[] { "IDMONAN", "IDMONAN2" });

            migrationBuilder.AddForeignKey(
                name: "FK_LISTGIASIZE_SanPhams_ProductsViewModelIDMONAN_ProductsViewModelIDMONAN2",
                table: "LISTGIASIZE",
                columns: new[] { "ProductsViewModelIDMONAN", "ProductsViewModelIDMONAN2" },
                principalTable: "SanPhams",
                principalColumns: new[] { "IDMONAN", "IDMONAN2" });

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhams_LOAIMONAN_CategoryIDLOAIMONAN",
                table: "SanPhams",
                column: "CategoryIDLOAIMONAN",
                principalTable: "LOAIMONAN",
                principalColumn: "IDLOAIMONAN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LISTGIASIZE_SanPhams_ProductsViewModelIDMONAN_ProductsViewModelIDMONAN2",
                table: "LISTGIASIZE");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhams_LOAIMONAN_CategoryIDLOAIMONAN",
                table: "SanPhams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SanPhams",
                table: "SanPhams");

            migrationBuilder.DropColumn(
                name: "IDLOAIMONANCHA",
                table: "LOAIMONAN");

            migrationBuilder.DropColumn(
                name: "IDLoaiMonAn",
                table: "SanPhams");

            migrationBuilder.RenameTable(
                name: "SanPhams",
                newName: "MONAN");

            migrationBuilder.RenameColumn(
                name: "IDSIZE",
                table: "SIZE",
                newName: "SIZE");

            migrationBuilder.RenameColumn(
                name: "CategoryIDLOAIMONAN",
                table: "MONAN",
                newName: "CategoryViewModelIDLOAIMONAN");

            migrationBuilder.RenameIndex(
                name: "IX_SanPhams_CategoryIDLOAIMONAN",
                table: "MONAN",
                newName: "IX_MONAN_CategoryViewModelIDLOAIMONAN");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MONAN",
                table: "MONAN",
                columns: new[] { "IDMONAN", "IDMONAN2" });

            migrationBuilder.AddForeignKey(
                name: "FK_LISTGIASIZE_MONAN_ProductsViewModelIDMONAN_ProductsViewModelIDMONAN2",
                table: "LISTGIASIZE",
                columns: new[] { "ProductsViewModelIDMONAN", "ProductsViewModelIDMONAN2" },
                principalTable: "MONAN",
                principalColumns: new[] { "IDMONAN", "IDMONAN2" });

            migrationBuilder.AddForeignKey(
                name: "FK_MONAN_LOAIMONAN_CategoryViewModelIDLOAIMONAN",
                table: "MONAN",
                column: "CategoryViewModelIDLOAIMONAN",
                principalTable: "LOAIMONAN",
                principalColumn: "IDLOAIMONAN");
        }
    }
}
