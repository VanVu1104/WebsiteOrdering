using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteOrdering.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "LOAIMONAN",
                columns: table => new
                {
                    IDLOAIMONAN = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TENLOAIMONAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IDLOAIMAN_CHA = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOAIMONAN", x => x.IDLOAIMONAN);
                });

            migrationBuilder.CreateTable(
                name: "SIZE",
                columns: table => new
                {
                    IDSIZE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TENSIZE = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIZE", x => x.IDSIZE);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IDDATBAN = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "MONAN",
                columns: table => new
                {
                    IDMONAN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TENMONAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MOTA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GIAMONAN = table.Column<int>(type: "int", nullable: false),
                    ANHMONAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TRANGTHAIMAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDLoaiMonAn = table.Column<string>(type: "nvarchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MONAN", x => x.IDMONAN);
                    table.ForeignKey(
                        name: "FK_MONAN_LOAIMONAN_IDLoaiMonAn",
                        column: x => x.IDLoaiMonAn,
                        principalTable: "LOAIMONAN",
                        principalColumn: "IDLOAIMONAN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHITIETDONHANG",
                columns: table => new
                {
                    IDDONHANG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDSIZE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDDEBANH = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false),
                    GIADH = table.Column<int>(type: "int", nullable: false),
                    TONGTIEN = table.Column<int>(type: "int", nullable: false),
                    GHICHU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KIEUPIZZA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonHangViewModelIDDONHANG = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETDONHANG", x => new { x.IDDONHANG, x.IDMONAN });
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
                        name: "FK_CHITIETDONHANG_MONAN_IDMONAN",
                        column: x => x.IDMONAN,
                        principalTable: "MONAN",
                        principalColumn: "IDMONAN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANG_SIZE_IDSIZE",
                        column: x => x.IDSIZE,
                        principalTable: "SIZE",
                        principalColumn: "IDSIZE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LISTGIASIZE",
                columns: table => new
                {
                    IDLOAIMONAN = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    IDSIZE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GIASIZE = table.Column<int>(type: "int", nullable: false),
                    ProductsViewModelIDMONAN = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SizeViewModelIDSIZE = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                        name: "FK_LISTGIASIZE_MONAN_ProductsViewModelIDMONAN",
                        column: x => x.ProductsViewModelIDMONAN,
                        principalTable: "MONAN",
                        principalColumn: "IDMONAN");
                    table.ForeignKey(
                        name: "FK_LISTGIASIZE_SIZE_IDSIZE",
                        column: x => x.IDSIZE,
                        principalTable: "SIZE",
                        principalColumn: "IDSIZE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LISTGIASIZE_SIZE_SizeViewModelIDSIZE",
                        column: x => x.SizeViewModelIDSIZE,
                        principalTable: "SIZE",
                        principalColumn: "IDSIZE");
                });

            migrationBuilder.CreateTable(
                name: "TOPPING",
                columns: table => new
                {
                    IDTOPPING = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TENTOPPING = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GIATOPPING = table.Column<int>(type: "int", nullable: false),
                    IDLOAIMONAN = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    CategoryViewModelIDLOAIMONAN = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    ProductsViewModelIDMONAN = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_TOPPING_MONAN_ProductsViewModelIDMONAN",
                        column: x => x.ProductsViewModelIDMONAN,
                        principalTable: "MONAN",
                        principalColumn: "IDMONAN");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETTOPPING",
                columns: table => new
                {
                    IDTOPING = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDDONHANG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDMONAN = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETTOPPING", x => new { x.IDTOPING, x.IDDONHANG, x.IDMONAN });
                    table.ForeignKey(
                        name: "FK_CHITIETTOPPING_CHITIETDONHANG_IDDONHANG_IDMONAN",
                        columns: x => new { x.IDDONHANG, x.IDMONAN },
                        principalTable: "CHITIETDONHANG",
                        principalColumns: new[] { "IDDONHANG", "IDMONAN" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETTOPPING_TOPPING_IDTOPING",
                        column: x => x.IDTOPING,
                        principalTable: "TOPPING",
                        principalColumn: "IDTOPPING",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANG_DonHangViewModelIDDONHANG",
                table: "CHITIETDONHANG",
                column: "DonHangViewModelIDDONHANG");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANG_IDDEBANH",
                table: "CHITIETDONHANG",
                column: "IDDEBANH");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANG_IDMONAN",
                table: "CHITIETDONHANG",
                column: "IDMONAN");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANG_IDSIZE",
                table: "CHITIETDONHANG",
                column: "IDSIZE");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETTOPPING_IDDONHANG_IDMONAN",
                table: "CHITIETTOPPING",
                columns: new[] { "IDDONHANG", "IDMONAN" });

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_IDCHINHANH",
                table: "DONHANG",
                column: "IDCHINHANH");

            migrationBuilder.CreateIndex(
                name: "IX_LISTGIASIZE_IDSIZE",
                table: "LISTGIASIZE",
                column: "IDSIZE");

            migrationBuilder.CreateIndex(
                name: "IX_LISTGIASIZE_ProductsViewModelIDMONAN",
                table: "LISTGIASIZE",
                column: "ProductsViewModelIDMONAN");

            migrationBuilder.CreateIndex(
                name: "IX_LISTGIASIZE_SizeViewModelIDSIZE",
                table: "LISTGIASIZE",
                column: "SizeViewModelIDSIZE");

            migrationBuilder.CreateIndex(
                name: "IX_MONAN_IDLoaiMonAn",
                table: "MONAN",
                column: "IDLoaiMonAn");

            migrationBuilder.CreateIndex(
                name: "IX_TOPPING_CategoryViewModelIDLOAIMONAN",
                table: "TOPPING",
                column: "CategoryViewModelIDLOAIMONAN");

            migrationBuilder.CreateIndex(
                name: "IX_TOPPING_IDLOAIMONAN",
                table: "TOPPING",
                column: "IDLOAIMONAN");

            migrationBuilder.CreateIndex(
                name: "IX_TOPPING_ProductsViewModelIDMONAN",
                table: "TOPPING",
                column: "ProductsViewModelIDMONAN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CHITIETTOPPING");

            migrationBuilder.DropTable(
                name: "LISTGIASIZE");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CHITIETDONHANG");

            migrationBuilder.DropTable(
                name: "TOPPING");

            migrationBuilder.DropTable(
                name: "DEBANH");

            migrationBuilder.DropTable(
                name: "DONHANG");

            migrationBuilder.DropTable(
                name: "SIZE");

            migrationBuilder.DropTable(
                name: "MONAN");

            migrationBuilder.DropTable(
                name: "CHINHANH");

            migrationBuilder.DropTable(
                name: "LOAIMONAN");
        }
    }
}
