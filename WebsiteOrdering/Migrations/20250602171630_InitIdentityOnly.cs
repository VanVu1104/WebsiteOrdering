using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteOrdering.Migrations
{
    /// <inheritdoc />
    public partial class InitIdentityOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_SanPhams_CategoryIDLOAIMONAN",
                table: "SanPhams");

            migrationBuilder.DropColumn(
                name: "CategoryIDLOAIMONAN",
                table: "SanPhams");

            migrationBuilder.RenameTable(
                name: "SanPhams",
                newName: "MONAN");

            migrationBuilder.AlterColumn<string>(
                name: "TENSIZE",
                table: "SIZE",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "IDSIZE",
                table: "SIZE",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "ProductsViewModelIDMONAN2",
                table: "LISTGIASIZE",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductsViewModelIDMONAN",
                table: "LISTGIASIZE",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IDSIZE",
                table: "LISTGIASIZE",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AddColumn<string>(
                name: "SizeViewModelIDSIZE",
                table: "LISTGIASIZE",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TRANGTHAI",
                table: "MONAN",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TENMONAN",
                table: "MONAN",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MOTAMONAN",
                table: "MONAN",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ANHMONAN",
                table: "MONAN",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IDMONAN2",
                table: "MONAN",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "IDMONAN",
                table: "MONAN",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MONAN",
                table: "MONAN",
                columns: new[] { "IDMONAN", "IDMONAN2" });

            migrationBuilder.CreateTable(
                name: "CHINHANH",
                columns: table => new
                {
                    IDCHINHANH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TENCHINHANH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DIACHICN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHINHANH", x => x.IDCHINHANH);
                });

            migrationBuilder.CreateTable(
                name: "DEBANH",
                columns: table => new
                {
                    IDDEBANH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TENDEBANH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GIADEBANH = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEBANH", x => x.IDDEBANH);
                });

            migrationBuilder.CreateTable(
                name: "TOPPING",
                columns: table => new
                {
                    IDTOPPING = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TENTOPPING = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GIATOPPING = table.Column<int>(type: "int", nullable: false),
                    IDLOAIMONAN = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    CategoryViewModelIDLOAIMONAN = table.Column<string>(type: "nvarchar(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOPPING", x => x.IDTOPPING);
                    table.ForeignKey(
                        name: "FK_TOPPING_LOAIMONAN_CategoryViewModelIDLOAIMONAN",
                        column: x => x.CategoryViewModelIDLOAIMONAN,
                        principalTable: "LOAIMONAN",
                        principalColumn: "IDLOAIMONAN");
                    table.ForeignKey(
                        name: "FK_TOPPING_LOAIMONAN_IDLOAIMONAN",
                        column: x => x.IDLOAIMONAN,
                        principalTable: "LOAIMONAN",
                        principalColumn: "IDLOAIMONAN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DONHANG",
                columns: table => new
                {
                    IDDONHANG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NGAYDAT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SONGUOI = table.Column<int>(type: "int", nullable: false),
                    TONGDH = table.Column<int>(type: "int", nullable: false),
                    TENKH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PHUONGTHUCTHANHTOAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDCHINHANH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDNV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDDATBAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDKHUYENMAI = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DONHANG", x => x.IDDONHANG);
                    table.ForeignKey(
                        name: "FK_DONHANG_CHINHANH_IDCHINHANH",
                        column: x => x.IDCHINHANH,
                        principalTable: "CHINHANH",
                        principalColumn: "IDCHINHANH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DONHANGONL",
                columns: table => new
                {
                    IDDONHANGONL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DIACHI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TRANGTHAI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TONGTIEN = table.Column<int>(type: "int", nullable: false),
                    NGAYDATDON = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PTTTONL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIENSHIP = table.Column<int>(type: "int", nullable: false),
                    IDKH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDCHINHANH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDKHUYENMAI = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DONHANGONL", x => x.IDDONHANGONL);
                    table.ForeignKey(
                        name: "FK_DONHANGONL_CHINHANH_IDCHINHANH",
                        column: x => x.IDCHINHANH,
                        principalTable: "CHINHANH",
                        principalColumn: "IDCHINHANH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHITIETDONHANG",
                columns: table => new
                {
                    IDDONHANG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN2 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDSIZE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDDEBANH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false),
                    GIA = table.Column<int>(type: "int", nullable: false),
                    TONGTIEN = table.Column<int>(type: "int", nullable: false),
                    GHICHU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KIEUPIZZA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonHangViewModelIDDONHANG = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETDONHANG", x => new { x.IDDONHANG, x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANG_DEBANH_IDDEBANH",
                        column: x => x.IDDEBANH,
                        principalTable: "DEBANH",
                        principalColumn: "IDDEBANH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANG_DONHANG_DonHangViewModelIDDONHANG",
                        column: x => x.DonHangViewModelIDDONHANG,
                        principalTable: "DONHANG",
                        principalColumn: "IDDONHANG");
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANG_DONHANG_IDDONHANG",
                        column: x => x.IDDONHANG,
                        principalTable: "DONHANG",
                        principalColumn: "IDDONHANG",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANG_MONAN_IDMONAN_IDMONAN2",
                        columns: x => new { x.IDMONAN, x.IDMONAN2 },
                        principalTable: "MONAN",
                        principalColumns: new[] { "IDMONAN", "IDMONAN2" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANG_SIZE_IDSIZE",
                        column: x => x.IDSIZE,
                        principalTable: "SIZE",
                        principalColumn: "IDSIZE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHITIETDONHANGONL",
                columns: table => new
                {
                    IDDONHANGONL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN2 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDSIZE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDDEBANH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SOLUONGDH = table.Column<int>(type: "int", nullable: false),
                    GIADH = table.Column<int>(type: "int", nullable: false),
                    TONGTIENDH = table.Column<int>(type: "int", nullable: false),
                    GHICHU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KIEUPIZZAONL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonHangOnlViewModelIDDONHANGONL = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETDONHANGONL", x => new { x.IDDONHANGONL, x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANGONL_DEBANH_IDDEBANH",
                        column: x => x.IDDEBANH,
                        principalTable: "DEBANH",
                        principalColumn: "IDDEBANH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANGONL_DONHANGONL_DonHangOnlViewModelIDDONHANGONL",
                        column: x => x.DonHangOnlViewModelIDDONHANGONL,
                        principalTable: "DONHANGONL",
                        principalColumn: "IDDONHANGONL");
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANGONL_DONHANGONL_IDDONHANGONL",
                        column: x => x.IDDONHANGONL,
                        principalTable: "DONHANGONL",
                        principalColumn: "IDDONHANGONL",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANGONL_MONAN_IDMONAN_IDMONAN2",
                        columns: x => new { x.IDMONAN, x.IDMONAN2 },
                        principalTable: "MONAN",
                        principalColumns: new[] { "IDMONAN", "IDMONAN2" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANGONL_SIZE_IDSIZE",
                        column: x => x.IDSIZE,
                        principalTable: "SIZE",
                        principalColumn: "IDSIZE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHITIETTOPPING",
                columns: table => new
                {
                    IDTOPING = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDDONHANG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN2 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETTOPPING", x => new { x.IDTOPING, x.IDDONHANG, x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK_CHITIETTOPPING_CHITIETDONHANG_IDDONHANG_IDMONAN_IDMONAN2",
                        columns: x => new { x.IDDONHANG, x.IDMONAN, x.IDMONAN2 },
                        principalTable: "CHITIETDONHANG",
                        principalColumns: new[] { "IDDONHANG", "IDMONAN", "IDMONAN2" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETTOPPING_TOPPING_IDTOPING",
                        column: x => x.IDTOPING,
                        principalTable: "TOPPING",
                        principalColumn: "IDTOPPING",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHITIETTOPPINGONL",
                columns: table => new
                {
                    IDTOPING = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDDONHANGONL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN2 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETTOPPINGONL", x => new { x.IDTOPING, x.IDDONHANGONL, x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK_CHITIETTOPPINGONL_CHITIETDONHANGONL_IDDONHANGONL_IDMONAN_IDMONAN2",
                        columns: x => new { x.IDDONHANGONL, x.IDMONAN, x.IDMONAN2 },
                        principalTable: "CHITIETDONHANGONL",
                        principalColumns: new[] { "IDDONHANGONL", "IDMONAN", "IDMONAN2" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETTOPPINGONL_TOPPING_IDTOPING",
                        column: x => x.IDTOPING,
                        principalTable: "TOPPING",
                        principalColumn: "IDTOPPING",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LISTGIASIZE_SizeViewModelIDSIZE",
                table: "LISTGIASIZE",
                column: "SizeViewModelIDSIZE");

            migrationBuilder.CreateIndex(
                name: "IX_MONAN_IDLoaiMonAn",
                table: "MONAN",
                column: "IDLoaiMonAn");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANG_DonHangViewModelIDDONHANG",
                table: "CHITIETDONHANG",
                column: "DonHangViewModelIDDONHANG");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANG_IDDEBANH",
                table: "CHITIETDONHANG",
                column: "IDDEBANH");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANG_IDMONAN_IDMONAN2",
                table: "CHITIETDONHANG",
                columns: new[] { "IDMONAN", "IDMONAN2" });

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANG_IDSIZE",
                table: "CHITIETDONHANG",
                column: "IDSIZE");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANGONL_DonHangOnlViewModelIDDONHANGONL",
                table: "CHITIETDONHANGONL",
                column: "DonHangOnlViewModelIDDONHANGONL");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANGONL_IDDEBANH",
                table: "CHITIETDONHANGONL",
                column: "IDDEBANH");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANGONL_IDMONAN_IDMONAN2",
                table: "CHITIETDONHANGONL",
                columns: new[] { "IDMONAN", "IDMONAN2" });

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANGONL_IDSIZE",
                table: "CHITIETDONHANGONL",
                column: "IDSIZE");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETTOPPING_IDDONHANG_IDMONAN_IDMONAN2",
                table: "CHITIETTOPPING",
                columns: new[] { "IDDONHANG", "IDMONAN", "IDMONAN2" });

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETTOPPINGONL_IDDONHANGONL_IDMONAN_IDMONAN2",
                table: "CHITIETTOPPINGONL",
                columns: new[] { "IDDONHANGONL", "IDMONAN", "IDMONAN2" });

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_IDCHINHANH",
                table: "DONHANG",
                column: "IDCHINHANH");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANGONL_IDCHINHANH",
                table: "DONHANGONL",
                column: "IDCHINHANH");

            migrationBuilder.CreateIndex(
                name: "IX_TOPPING_CategoryViewModelIDLOAIMONAN",
                table: "TOPPING",
                column: "CategoryViewModelIDLOAIMONAN");

            migrationBuilder.CreateIndex(
                name: "IX_TOPPING_IDLOAIMONAN",
                table: "TOPPING",
                column: "IDLOAIMONAN");

            migrationBuilder.AddForeignKey(
                name: "FK_LISTGIASIZE_MONAN_ProductsViewModelIDMONAN_ProductsViewModelIDMONAN2",
                table: "LISTGIASIZE",
                columns: new[] { "ProductsViewModelIDMONAN", "ProductsViewModelIDMONAN2" },
                principalTable: "MONAN",
                principalColumns: new[] { "IDMONAN", "IDMONAN2" });

            migrationBuilder.AddForeignKey(
                name: "FK_LISTGIASIZE_SIZE_SizeViewModelIDSIZE",
                table: "LISTGIASIZE",
                column: "SizeViewModelIDSIZE",
                principalTable: "SIZE",
                principalColumn: "IDSIZE");

            migrationBuilder.AddForeignKey(
                name: "FK_MONAN_LOAIMONAN_IDLoaiMonAn",
                table: "MONAN",
                column: "IDLoaiMonAn",
                principalTable: "LOAIMONAN",
                principalColumn: "IDLOAIMONAN",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LISTGIASIZE_MONAN_ProductsViewModelIDMONAN_ProductsViewModelIDMONAN2",
                table: "LISTGIASIZE");

            migrationBuilder.DropForeignKey(
                name: "FK_LISTGIASIZE_SIZE_SizeViewModelIDSIZE",
                table: "LISTGIASIZE");

            migrationBuilder.DropForeignKey(
                name: "FK_MONAN_LOAIMONAN_IDLoaiMonAn",
                table: "MONAN");

            migrationBuilder.DropTable(
                name: "CHITIETTOPPING");

            migrationBuilder.DropTable(
                name: "CHITIETTOPPINGONL");

            migrationBuilder.DropTable(
                name: "CHITIETDONHANG");

            migrationBuilder.DropTable(
                name: "CHITIETDONHANGONL");

            migrationBuilder.DropTable(
                name: "TOPPING");

            migrationBuilder.DropTable(
                name: "DONHANG");

            migrationBuilder.DropTable(
                name: "DEBANH");

            migrationBuilder.DropTable(
                name: "DONHANGONL");

            migrationBuilder.DropTable(
                name: "CHINHANH");

            migrationBuilder.DropIndex(
                name: "IX_LISTGIASIZE_SizeViewModelIDSIZE",
                table: "LISTGIASIZE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MONAN",
                table: "MONAN");

            migrationBuilder.DropIndex(
                name: "IX_MONAN_IDLoaiMonAn",
                table: "MONAN");

            migrationBuilder.DropColumn(
                name: "SizeViewModelIDSIZE",
                table: "LISTGIASIZE");

            migrationBuilder.RenameTable(
                name: "MONAN",
                newName: "SanPhams");

            migrationBuilder.AlterColumn<string>(
                name: "TENSIZE",
                table: "SIZE",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IDSIZE",
                table: "SIZE",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductsViewModelIDMONAN2",
                table: "LISTGIASIZE",
                type: "nvarchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductsViewModelIDMONAN",
                table: "LISTGIASIZE",
                type: "nvarchar(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IDSIZE",
                table: "LISTGIASIZE",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TRANGTHAI",
                table: "SanPhams",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TENMONAN",
                table: "SanPhams",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MOTAMONAN",
                table: "SanPhams",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ANHMONAN",
                table: "SanPhams",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IDMONAN2",
                table: "SanPhams",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "IDMONAN",
                table: "SanPhams",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CategoryIDLOAIMONAN",
                table: "SanPhams",
                type: "nvarchar(5)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SanPhams",
                table: "SanPhams",
                columns: new[] { "IDMONAN", "IDMONAN2" });

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_CategoryIDLOAIMONAN",
                table: "SanPhams",
                column: "CategoryIDLOAIMONAN");

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
    }
}
