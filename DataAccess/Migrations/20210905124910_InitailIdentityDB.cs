using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitailIdentityDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analize",
                columns: table => new
                {
                    SifraAnalize = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivAnalize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonjaGranica = table.Column<double>(type: "float", nullable: false),
                    DonjaGranicaJedinicaMere = table.Column<int>(type: "int", nullable: false),
                    GornjaGranica = table.Column<double>(type: "float", nullable: false),
                    GornjaGranicaJedinicaMere = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analize", x => x.SifraAnalize);
                });

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
                name: "Mesta",
                columns: table => new
                {
                    Sifra = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesta", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "Usluge",
                columns: table => new
                {
                    Sifra = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluge", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "Ustanove",
                columns: table => new
                {
                    Sifra = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ustanove", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "VrsteTerapija",
                columns: table => new
                {
                    Sifra = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteTerapija", x => x.Sifra);
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
                name: "Pacijenti",
                columns: table => new
                {
                    JmbgPacijenta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImePrezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pol = table.Column<int>(type: "int", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SifraMesta = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacijenti", x => x.JmbgPacijenta);
                    table.ForeignKey(
                        name: "FK_Pacijenti_Mesta_SifraMesta",
                        column: x => x.SifraMesta,
                        principalTable: "Mesta",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZdravstveniRadnici",
                columns: table => new
                {
                    RadnikID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImePrezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pol = table.Column<int>(type: "int", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StepenObrazovanja = table.Column<int>(type: "int", nullable: false),
                    SifraMesta = table.Column<long>(type: "bigint", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZdravstveniRadnici", x => x.RadnikID);
                    table.ForeignKey(
                        name: "FK_ZdravstveniRadnici_Mesta_SifraMesta",
                        column: x => x.SifraMesta,
                        principalTable: "Mesta",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UputiZaTerapiju",
                columns: table => new
                {
                    BrojUputa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RokVazenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SifraUstanove = table.Column<long>(type: "bigint", nullable: false),
                    RedniBrojZahteva = table.Column<int>(type: "int", nullable: false),
                    SifraTerapije = table.Column<long>(type: "bigint", nullable: false),
                    JmbgPacijenta = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UputiZaTerapiju", x => x.BrojUputa);
                    table.ForeignKey(
                        name: "FK_UputiZaTerapiju_Pacijenti_JmbgPacijenta",
                        column: x => x.JmbgPacijenta,
                        principalTable: "Pacijenti",
                        principalColumn: "JmbgPacijenta",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UputiZaTerapiju_Ustanove_SifraUstanove",
                        column: x => x.SifraUstanove,
                        principalTable: "Ustanove",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UputiZaTerapiju_VrsteTerapija_SifraTerapije",
                        column: x => x.SifraTerapije,
                        principalTable: "VrsteTerapija",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZdravstveneKnjizice",
                columns: table => new
                {
                    BrojKnjizice = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JmbgPacijenta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BrojZdravstvenogOsiguranja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LBO = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZdravstveneKnjizice", x => new { x.BrojKnjizice, x.JmbgPacijenta });
                    table.ForeignKey(
                        name: "FK_ZdravstveneKnjizice_Pacijenti_JmbgPacijenta",
                        column: x => x.JmbgPacijenta,
                        principalTable: "Pacijenti",
                        principalColumn: "JmbgPacijenta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TerminiTerapije",
                columns: table => new
                {
                    RadnikId = table.Column<long>(type: "bigint", nullable: false),
                    VremeDatumTerapije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false),
                    SifraUsluge = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminiTerapije", x => new { x.VremeDatumTerapije, x.RadnikId });
                    table.ForeignKey(
                        name: "FK_TerminiTerapije_Usluge_SifraUsluge",
                        column: x => x.SifraUsluge,
                        principalTable: "Usluge",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerminiTerapije_ZdravstveniRadnici_RadnikId",
                        column: x => x.RadnikId,
                        principalTable: "ZdravstveniRadnici",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KarticeZaEvidenciju",
                columns: table => new
                {
                    SifraKartice = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumIzdavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojUputa = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SifraUsluge = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KarticeZaEvidenciju", x => x.SifraKartice);
                    table.ForeignKey(
                        name: "FK_KarticeZaEvidenciju_UputiZaTerapiju_BrojUputa",
                        column: x => x.BrojUputa,
                        principalTable: "UputiZaTerapiju",
                        principalColumn: "BrojUputa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KarticeZaEvidenciju_Usluge_SifraUsluge",
                        column: x => x.SifraUsluge,
                        principalTable: "Usluge",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvidencijaTermina",
                columns: table => new
                {
                    Sifra = table.Column<long>(type: "bigint", nullable: false),
                    RadnikId = table.Column<long>(type: "bigint", nullable: false),
                    VremeDatumTerapije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvidencijaTermina", x => new { x.Sifra, x.VremeDatumTerapije, x.RadnikId });
                    table.ForeignKey(
                        name: "FK_EvidencijaTermina_KarticeZaEvidenciju_Sifra",
                        column: x => x.Sifra,
                        principalTable: "KarticeZaEvidenciju",
                        principalColumn: "SifraKartice",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvidencijaTermina_TerminiTerapije_VremeDatumTerapije_RadnikId",
                        columns: x => new { x.VremeDatumTerapije, x.RadnikId },
                        principalTable: "TerminiTerapije",
                        principalColumns: new[] { "VremeDatumTerapije", "RadnikId" },
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_EvidencijaTermina_VremeDatumTerapije_RadnikId",
                table: "EvidencijaTermina",
                columns: new[] { "VremeDatumTerapije", "RadnikId" });

            migrationBuilder.CreateIndex(
                name: "IX_KarticeZaEvidenciju_BrojUputa",
                table: "KarticeZaEvidenciju",
                column: "BrojUputa",
                unique: true,
                filter: "[BrojUputa] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KarticeZaEvidenciju_SifraUsluge",
                table: "KarticeZaEvidenciju",
                column: "SifraUsluge",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacijenti_SifraMesta",
                table: "Pacijenti",
                column: "SifraMesta");

            migrationBuilder.CreateIndex(
                name: "IX_TerminiTerapije_RadnikId",
                table: "TerminiTerapije",
                column: "RadnikId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminiTerapije_SifraUsluge",
                table: "TerminiTerapije",
                column: "SifraUsluge");

            migrationBuilder.CreateIndex(
                name: "IX_UputiZaTerapiju_JmbgPacijenta",
                table: "UputiZaTerapiju",
                column: "JmbgPacijenta",
                unique: true,
                filter: "[JmbgPacijenta] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UputiZaTerapiju_SifraTerapije",
                table: "UputiZaTerapiju",
                column: "SifraTerapije");

            migrationBuilder.CreateIndex(
                name: "IX_UputiZaTerapiju_SifraUstanove",
                table: "UputiZaTerapiju",
                column: "SifraUstanove");

            migrationBuilder.CreateIndex(
                name: "IX_ZdravstveneKnjizice_JmbgPacijenta",
                table: "ZdravstveneKnjizice",
                column: "JmbgPacijenta");

            migrationBuilder.CreateIndex(
                name: "IX_ZdravstveniRadnici_SifraMesta",
                table: "ZdravstveniRadnici",
                column: "SifraMesta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analize");

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
                name: "EvidencijaTermina");

            migrationBuilder.DropTable(
                name: "ZdravstveneKnjizice");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "KarticeZaEvidenciju");

            migrationBuilder.DropTable(
                name: "TerminiTerapije");

            migrationBuilder.DropTable(
                name: "UputiZaTerapiju");

            migrationBuilder.DropTable(
                name: "Usluge");

            migrationBuilder.DropTable(
                name: "ZdravstveniRadnici");

            migrationBuilder.DropTable(
                name: "Pacijenti");

            migrationBuilder.DropTable(
                name: "Ustanove");

            migrationBuilder.DropTable(
                name: "VrsteTerapija");

            migrationBuilder.DropTable(
                name: "Mesta");
        }
    }
}
