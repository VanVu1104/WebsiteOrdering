using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteOrdering.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentity : Migration
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
                name: "CHINHANH",
                columns: table => new
                {
                    IDCHINHANH = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    TENCHINHANH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DIACHICN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHINHANH__5F20FC4041DF698B", x => x.IDCHINHANH);
                });

            migrationBuilder.CreateTable(
                name: "ChitietdonhangonlTopping",
                columns: table => new
                {
                    Idtopping = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Iddonhangonl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Idmonan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Idmonan2 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChitietdonhangonlTopping", x => new { x.Idtopping, x.Iddonhangonl, x.Idmonan, x.Idmonan2 });
                });

            migrationBuilder.CreateTable(
                name: "ChitietdonhangTopping",
                columns: table => new
                {
                    Idtopping = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Iddonhang = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Idmonan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Idmonan2 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChitietdonhangTopping", x => new { x.Idtopping, x.Iddonhang, x.Idmonan, x.Idmonan2 });
                });

            migrationBuilder.CreateTable(
                name: "DANHMUCKHUYENMAI",
                columns: table => new
                {
                    IDKHUYENMAI = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    TENKHUYENMAI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NGAYAPDUNG = table.Column<DateOnly>(type: "date", nullable: false),
                    NGAYHETHAN = table.Column<DateOnly>(type: "date", nullable: false),
                    GIATRI = table.Column<int>(type: "int", nullable: false),
                    MOTAKM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DANHMUCK__9E055897C118320B", x => x.IDKHUYENMAI);
                });

            migrationBuilder.CreateTable(
                name: "DEBANH",
                columns: table => new
                {
                    IDDEBANH = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    TENDEBANH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GIADEBANH = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DEBANH__555F23FFB140968E", x => x.IDDEBANH);
                });

            migrationBuilder.CreateTable(
                name: "LOAIMONAN",
                columns: table => new
                {
                    IDLOAIMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    TENLOAIMONAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LOAIMONA__6B7E94ED8043D23A", x => x.IDLOAIMONAN);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", fixedLength: true, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ID__6B7E94ED8043D11B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SIZE",
                columns: table => new
                {
                    IDSIZE = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    TENSIZE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SIZE__8DA14C4E58CC03AA", x => x.IDSIZE);
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NGAYSINH = table.Column<DateOnly>(type: "date", nullable: true),
                    GIOITINH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IDCHINHANH = table.Column<string>(type: "char(5)", unicode: false, maxLength: 5, nullable: true),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_CHINHANH_IDCHINHANH",
                        column: x => x.IDCHINHANH,
                        principalTable: "CHINHANH",
                        principalColumn: "IDCHINHANH");
                });

            migrationBuilder.CreateTable(
                name: "BAN",
                columns: table => new
                {
                    IDBAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    TENBAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SONGUOI = table.Column<int>(type: "int", nullable: false),
                    KHUVUC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IDCHINHANH = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BAN__9367225EC3E6D028", x => x.IDBAN);
                    table.ForeignKey(
                        name: "FK__BAN__IDCHINHANH__6477ECF3",
                        column: x => x.IDCHINHANH,
                        principalTable: "CHINHANH",
                        principalColumn: "IDCHINHANH");
                });

            migrationBuilder.CreateTable(
                name: "MONAN",
                columns: table => new
                {
                    IDMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDMONAN2 = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    TENMONAN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MOTAMONAN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GIACOBAN = table.Column<int>(type: "int", nullable: false),
                    ANHMONAN = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    TRANGTHAI = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IDLOAIMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MONAN__B62500C613AC53C3", x => new { x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK__MONAN__IDLOAIMON__06CD04F7",
                        column: x => x.IDLOAIMONAN,
                        principalTable: "LOAIMONAN",
                        principalColumn: "IDLOAIMONAN");
                });

            migrationBuilder.CreateTable(
                name: "TOPPING",
                columns: table => new
                {
                    IDTOPPING = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    TENTOPPING = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GIATOPPING = table.Column<int>(type: "int", nullable: false),
                    IDLOAIMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TOPPING__B17F5B459FE8393F", x => x.IDTOPPING);
                    table.ForeignKey(
                        name: "FK__TOPPING__IDLOAIM__6B24EA82",
                        column: x => x.IDLOAIMONAN,
                        principalTable: "LOAIMONAN",
                        principalColumn: "IDLOAIMONAN");
                });

            migrationBuilder.CreateTable(
                name: "LISTGIASIZE",
                columns: table => new
                {
                    IDLOAIMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDSIZE = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    GIA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LISTGIAS__93A480291F324B05", x => new { x.IDLOAIMONAN, x.IDSIZE });
                    table.ForeignKey(
                        name: "FK__LISTGIASI__IDLOA__71D1E811",
                        column: x => x.IDLOAIMONAN,
                        principalTable: "LOAIMONAN",
                        principalColumn: "IDLOAIMONAN");
                    table.ForeignKey(
                        name: "FK__LISTGIASI__IDSIZ__72C60C4A",
                        column: x => x.IDSIZE,
                        principalTable: "SIZE",
                        principalColumn: "IDSIZE");
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
                name: "DATBAN",
                columns: table => new
                {
                    IDDATBAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    NGAYDAT = table.Column<DateOnly>(type: "date", nullable: false),
                    GIOBATDAU = table.Column<TimeOnly>(type: "time", nullable: false),
                    GIOKETTHUC = table.Column<TimeOnly>(type: "time", nullable: false),
                    SONGUOIDAT = table.Column<int>(type: "int", nullable: false),
                    GHICHU = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TRANGTHAIDATBAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IDCHINHANH = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    USERID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DATBAN__DEC46009C032B7D1", x => x.IDDATBAN);
                    table.ForeignKey(
                        name: "FK__DATBAN__IDCHINHA__60A75C0F",
                        column: x => x.IDCHINHANH,
                        principalTable: "CHINHANH",
                        principalColumn: "IDCHINHANH");
                    table.ForeignKey(
                        name: "FK__DATBAN__USERID__619B8048",
                        column: x => x.USERID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DONHANGONL",
                columns: table => new
                {
                    IDDONHANGONL = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    DIACHI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TRANGTHAI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TONGTIEN = table.Column<int>(type: "int", nullable: false),
                    NGAYDATDON = table.Column<DateOnly>(type: "date", nullable: false),
                    PTTTONL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TIENSHIP = table.Column<int>(type: "int", nullable: false),
                    USERID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IDCHINHANH = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDKHUYENMAI = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DONHANGO__AC2A315C15B12C03", x => x.IDDONHANGONL);
                    table.ForeignKey(
                        name: "FK__DONHANGONL__USERID__02084FDA",
                        column: x => x.USERID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__DONHANGON__IDCHI__02FC7413",
                        column: x => x.IDCHINHANH,
                        principalTable: "CHINHANH",
                        principalColumn: "IDCHINHANH");
                    table.ForeignKey(
                        name: "FK__DONHANGON__IDKHU__03F0984C",
                        column: x => x.IDKHUYENMAI,
                        principalTable: "DANHMUCKHUYENMAI",
                        principalColumn: "IDKHUYENMAI");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETBAN",
                columns: table => new
                {
                    IDDATBAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDBAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    GIOVAO = table.Column<TimeOnly>(type: "time", nullable: false),
                    GIORA = table.Column<TimeOnly>(type: "time", nullable: false),
                    TRANGTHAIBAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHITIETB__27F2122C9119BD2E", x => new { x.IDDATBAN, x.IDBAN });
                    table.ForeignKey(
                        name: "FK__CHITIETBA__IDBAN__76969D2E",
                        column: x => x.IDBAN,
                        principalTable: "BAN",
                        principalColumn: "IDBAN");
                    table.ForeignKey(
                        name: "FK__CHITIETBA__IDDAT__75A278F5",
                        column: x => x.IDDATBAN,
                        principalTable: "DATBAN",
                        principalColumn: "IDDATBAN");
                });

            migrationBuilder.CreateTable(
                name: "DONHANG",
                columns: table => new
                {
                    IDDONHANG = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    NGAYDAT = table.Column<DateOnly>(type: "date", nullable: false),
                    SONGUOI = table.Column<int>(type: "int", nullable: false),
                    TONGDH = table.Column<int>(type: "int", nullable: false),
                    TENKH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PHUONGTHUCTHANHTOAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IDCHINHANH = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    USERID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IDDATBAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDKHUYENMAI = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DONHANG__F59FA8B118A5D193", x => x.IDDONHANG);
                    table.ForeignKey(
                        name: "FK__DONHANG__IDCHINH__7C4F7684",
                        column: x => x.IDCHINHANH,
                        principalTable: "CHINHANH",
                        principalColumn: "IDCHINHANH");
                    table.ForeignKey(
                        name: "FK__DONHANG__IDDATBA__7E37BEF6",
                        column: x => x.IDDATBAN,
                        principalTable: "DATBAN",
                        principalColumn: "IDDATBAN");
                    table.ForeignKey(
                        name: "FK__DONHANG__IDKHUYE__7F2BE32F",
                        column: x => x.IDKHUYENMAI,
                        principalTable: "DANHMUCKHUYENMAI",
                        principalColumn: "IDKHUYENMAI");
                    table.ForeignKey(
                        name: "FK__DONHANG__USERID__7D439ABD",
                        column: x => x.USERID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETDONHANGONL",
                columns: table => new
                {
                    IDDONHANGONL = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDMONAN2 = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    SOLUONGDH = table.Column<int>(type: "int", nullable: false),
                    GIADH = table.Column<int>(type: "int", nullable: false),
                    TONGTIENDH = table.Column<int>(type: "int", nullable: false),
                    GHICHU = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KIEUPIZZAONL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDDEBANH = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    IDSIZE = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHITIETD__D7486150E300646B", x => new { x.IDDONHANGONL, x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK__CHITIETDONHANGON__123EB7A3",
                        columns: x => new { x.IDMONAN, x.IDMONAN2 },
                        principalTable: "MONAN",
                        principalColumns: new[] { "IDMONAN", "IDMONAN2" });
                    table.ForeignKey(
                        name: "FK__CHITIETDO__IDDEB__10566F31",
                        column: x => x.IDDEBANH,
                        principalTable: "DEBANH",
                        principalColumn: "IDDEBANH");
                    table.ForeignKey(
                        name: "FK__CHITIETDO__IDDON__0F624AF8",
                        column: x => x.IDDONHANGONL,
                        principalTable: "DONHANGONL",
                        principalColumn: "IDDONHANGONL");
                    table.ForeignKey(
                        name: "FK__CHITIETDO__IDSIZ__114A936A",
                        column: x => x.IDSIZE,
                        principalTable: "SIZE",
                        principalColumn: "IDSIZE");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETDONHANG",
                columns: table => new
                {
                    IDDONHANG = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDMONAN2 = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false),
                    GIA = table.Column<int>(type: "int", nullable: false),
                    TONGTIEN = table.Column<int>(type: "int", nullable: false),
                    GHICHU = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KIEUPIZZA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDDEBANH = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    IDSIZE = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHITIETD__8EFDF8BDABAA408C", x => new { x.IDDONHANG, x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK__CHITIETDONHANG__0B91BA14",
                        columns: x => new { x.IDMONAN, x.IDMONAN2 },
                        principalTable: "MONAN",
                        principalColumns: new[] { "IDMONAN", "IDMONAN2" });
                    table.ForeignKey(
                        name: "FK__CHITIETDO__IDDEB__0A9D95DB",
                        column: x => x.IDDEBANH,
                        principalTable: "DEBANH",
                        principalColumn: "IDDEBANH");
                    table.ForeignKey(
                        name: "FK__CHITIETDO__IDDON__09A971A2",
                        column: x => x.IDDONHANG,
                        principalTable: "DONHANG",
                        principalColumn: "IDDONHANG");
                    table.ForeignKey(
                        name: "FK__CHITIETDO__IDSIZ__0C85DE4D",
                        column: x => x.IDSIZE,
                        principalTable: "SIZE",
                        principalColumn: "IDSIZE");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETTOPPINGONL",
                columns: table => new
                {
                    IDTOPPING = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDDONHANGONL = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDMONAN2 = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHITIETT__AC0BDD50F7A79B3B", x => new { x.IDTOPPING, x.IDDONHANGONL, x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK__CHITIETTOPPINGON__19DFD96B",
                        columns: x => new { x.IDDONHANGONL, x.IDMONAN, x.IDMONAN2 },
                        principalTable: "CHITIETDONHANGONL",
                        principalColumns: new[] { "IDDONHANGONL", "IDMONAN", "IDMONAN2" });
                    table.ForeignKey(
                        name: "FK__CHITIETTO__IDTOP__18EBB532",
                        column: x => x.IDTOPPING,
                        principalTable: "TOPPING",
                        principalColumn: "IDTOPPING");
                });

            migrationBuilder.CreateTable(
                name: "CHITIETTOPPING",
                columns: table => new
                {
                    IDTOPPING = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDDONHANG = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDMONAN = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IDMONAN2 = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHITIETT__799084CEE74352D4", x => new { x.IDTOPPING, x.IDDONHANG, x.IDMONAN, x.IDMONAN2 });
                    table.ForeignKey(
                        name: "FK__CHITIETTOPPING__160F4887",
                        columns: x => new { x.IDDONHANG, x.IDMONAN, x.IDMONAN2 },
                        principalTable: "CHITIETDONHANG",
                        principalColumns: new[] { "IDDONHANG", "IDMONAN", "IDMONAN2" });
                    table.ForeignKey(
                        name: "FK__CHITIETTO__IDTOP__151B244E",
                        column: x => x.IDTOPPING,
                        principalTable: "TOPPING",
                        principalColumn: "IDTOPPING");
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
                name: "IX_AspNetUsers_IDCHINHANH",
                table: "AspNetUsers",
                column: "IDCHINHANH");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BAN_IDCHINHANH",
                table: "BAN",
                column: "IDCHINHANH");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETBAN_IDBAN",
                table: "CHITIETBAN",
                column: "IDBAN");

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
                name: "IX_DATBAN_IDCHINHANH",
                table: "DATBAN",
                column: "IDCHINHANH");

            migrationBuilder.CreateIndex(
                name: "IX_DATBAN_USERID",
                table: "DATBAN",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_IDCHINHANH",
                table: "DONHANG",
                column: "IDCHINHANH");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_IDDATBAN",
                table: "DONHANG",
                column: "IDDATBAN");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_IDKHUYENMAI",
                table: "DONHANG",
                column: "IDKHUYENMAI");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_USERID",
                table: "DONHANG",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANGONL_IDCHINHANH",
                table: "DONHANGONL",
                column: "IDCHINHANH");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANGONL_IDKHUYENMAI",
                table: "DONHANGONL",
                column: "IDKHUYENMAI");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANGONL_USERID",
                table: "DONHANGONL",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_LISTGIASIZE_IDSIZE",
                table: "LISTGIASIZE",
                column: "IDSIZE");

            migrationBuilder.CreateIndex(
                name: "IX_MONAN_IDLOAIMONAN",
                table: "MONAN",
                column: "IDLOAIMONAN");

            migrationBuilder.CreateIndex(
                name: "IX_TOPPING_IDLOAIMONAN",
                table: "TOPPING",
                column: "IDLOAIMONAN");
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
                name: "CHITIETBAN");

            migrationBuilder.DropTable(
                name: "ChitietdonhangonlTopping");

            migrationBuilder.DropTable(
                name: "ChitietdonhangTopping");

            migrationBuilder.DropTable(
                name: "CHITIETTOPPING");

            migrationBuilder.DropTable(
                name: "CHITIETTOPPINGONL");

            migrationBuilder.DropTable(
                name: "LISTGIASIZE");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BAN");

            migrationBuilder.DropTable(
                name: "CHITIETDONHANG");

            migrationBuilder.DropTable(
                name: "CHITIETDONHANGONL");

            migrationBuilder.DropTable(
                name: "TOPPING");

            migrationBuilder.DropTable(
                name: "DONHANG");

            migrationBuilder.DropTable(
                name: "MONAN");

            migrationBuilder.DropTable(
                name: "DEBANH");

            migrationBuilder.DropTable(
                name: "DONHANGONL");

            migrationBuilder.DropTable(
                name: "SIZE");

            migrationBuilder.DropTable(
                name: "DATBAN");

            migrationBuilder.DropTable(
                name: "LOAIMONAN");

            migrationBuilder.DropTable(
                name: "DANHMUCKHUYENMAI");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CHINHANH");
        }
    }
}
