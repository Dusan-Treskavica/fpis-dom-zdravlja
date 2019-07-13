using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FPIS_Projekat.DataAccess.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analize",
                columns: table => new
                {
                    SifraAnalize = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DonjaGranica = table.Column<double>(nullable: false),
                    DonjaGranicaJedinicaMere = table.Column<int>(nullable: false),
                    GornjaGranica = table.Column<double>(nullable: false),
                    GornjaGranicaJedinicaMere = table.Column<int>(nullable: false),
                    NazivAnalize = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analize", x => x.SifraAnalize);
                });

            migrationBuilder.CreateTable(
                name: "Mesta",
                columns: table => new
                {
                    Sifra = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesta", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "Usluge",
                columns: table => new
                {
                    Sifra = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluge", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "Ustanove",
                columns: table => new
                {
                    Sifra = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ustanove", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "VrsteTerapija",
                columns: table => new
                {
                    Sifra = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteTerapija", x => x.Sifra);
                });

            migrationBuilder.CreateTable(
                name: "Pacijenti",
                columns: table => new
                {
                    JmbgPacijenta = table.Column<string>(nullable: false),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    ImePrezime = table.Column<string>(nullable: true),
                    Pol = table.Column<int>(nullable: false),
                    SifraMesta = table.Column<long>(nullable: false),
                    Telefon = table.Column<string>(nullable: true)
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
                    RadnikID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumRodjenja = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ImePrezime = table.Column<string>(nullable: true),
                    JMBG = table.Column<string>(nullable: true),
                    Pol = table.Column<int>(nullable: false),
                    SifraMesta = table.Column<long>(nullable: false),
                    StepenObrazovanja = table.Column<int>(nullable: false)
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
                    BrojUputa = table.Column<string>(nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(nullable: false),
                    JmbgPacijenta = table.Column<string>(nullable: true),
                    RedniBrojZahteva = table.Column<int>(nullable: false),
                    RokVazenja = table.Column<DateTime>(nullable: false),
                    SifraTerapije = table.Column<long>(nullable: false),
                    SifraUstanove = table.Column<long>(nullable: false)
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
                        name: "FK_UputiZaTerapiju_VrsteTerapija_SifraTerapije",
                        column: x => x.SifraTerapije,
                        principalTable: "VrsteTerapija",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UputiZaTerapiju_Ustanove_SifraUstanove",
                        column: x => x.SifraUstanove,
                        principalTable: "Ustanove",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZdravstveneKnjizice",
                columns: table => new
                {
                    BrojKnjizice = table.Column<string>(nullable: false),
                    JmbgPacijenta = table.Column<string>(nullable: false),
                    BrojZdravstvenogOsiguranja = table.Column<string>(nullable: true),
                    LBO = table.Column<string>(nullable: true)
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
                    VremeDatumTerapije = table.Column<DateTime>(nullable: false),
                    RadnikId = table.Column<long>(nullable: false),
                    Kapacitet = table.Column<int>(nullable: false),
                    SifraUsluge = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminiTerapije", x => new { x.VremeDatumTerapije, x.RadnikId });
                    table.ForeignKey(
                        name: "FK_TerminiTerapije_ZdravstveniRadnici_RadnikId",
                        column: x => x.RadnikId,
                        principalTable: "ZdravstveniRadnici",
                        principalColumn: "RadnikID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TerminiTerapije_Usluge_SifraUsluge",
                        column: x => x.SifraUsluge,
                        principalTable: "Usluge",
                        principalColumn: "Sifra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KarticeZaEvidenciju",
                columns: table => new
                {
                    SifraKartice = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojUputa = table.Column<string>(nullable: true),
                    DatumIzdavanja = table.Column<DateTime>(nullable: false),
                    SifraUsluge = table.Column<long>(nullable: false)
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
                    Sifra = table.Column<long>(nullable: false),
                    VremeDatumTerapije = table.Column<DateTime>(nullable: false),
                    RadnikId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false)
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
                name: "EvidencijaTermina");

            migrationBuilder.DropTable(
                name: "ZdravstveneKnjizice");

            migrationBuilder.DropTable(
                name: "KarticeZaEvidenciju");

            migrationBuilder.DropTable(
                name: "TerminiTerapije");

            migrationBuilder.DropTable(
                name: "UputiZaTerapiju");

            migrationBuilder.DropTable(
                name: "ZdravstveniRadnici");

            migrationBuilder.DropTable(
                name: "Usluge");

            migrationBuilder.DropTable(
                name: "Pacijenti");

            migrationBuilder.DropTable(
                name: "VrsteTerapija");

            migrationBuilder.DropTable(
                name: "Ustanove");

            migrationBuilder.DropTable(
                name: "Mesta");
        }
    }
}
