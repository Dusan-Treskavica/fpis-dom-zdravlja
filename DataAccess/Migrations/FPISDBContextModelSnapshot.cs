﻿// <auto-generated />
using System;
using DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(FPISDBContext))]
    partial class FPISDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Entities.Analiza", b =>
                {
                    b.Property<long>("SifraAnalize")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("DonjaGranica")
                        .HasColumnType("float");

                    b.Property<int>("DonjaGranicaJedinicaMere")
                        .HasColumnType("int");

                    b.Property<double>("GornjaGranica")
                        .HasColumnType("float");

                    b.Property<int>("GornjaGranicaJedinicaMere")
                        .HasColumnType("int");

                    b.Property<string>("NazivAnalize")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SifraAnalize");

                    b.ToTable("Analize");
                });

            modelBuilder.Entity("DataAccess.Entities.Auth.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DataAccess.Entities.Auth.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("DataAccess.Entities.EvidencijaTermina", b =>
                {
                    b.Property<long>("Sifra")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("VremeDatumTerapije")
                        .HasColumnType("datetime2");

                    b.Property<long>("RadnikId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Sifra", "VremeDatumTerapije", "RadnikId");

                    b.HasIndex("VremeDatumTerapije", "RadnikId");

                    b.ToTable("EvidencijaTermina");
                });

            modelBuilder.Entity("DataAccess.Entities.KarticaZaEvidenciju", b =>
                {
                    b.Property<long>("SifraKartice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrojUputa")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DatumIzdavanja")
                        .HasColumnType("datetime2");

                    b.Property<long>("SifraUsluge")
                        .HasColumnType("bigint");

                    b.HasKey("SifraKartice");

                    b.HasIndex("BrojUputa")
                        .IsUnique()
                        .HasFilter("[BrojUputa] IS NOT NULL");

                    b.HasIndex("SifraUsluge")
                        .IsUnique();

                    b.ToTable("KarticeZaEvidenciju");
                });

            modelBuilder.Entity("DataAccess.Entities.Mesto", b =>
                {
                    b.Property<long>("Sifra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sifra");

                    b.ToTable("Mesta");
                });

            modelBuilder.Entity("DataAccess.Entities.Pacijent", b =>
                {
                    b.Property<string>("JmbgPacijenta")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImePrezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pol")
                        .HasColumnType("int");

                    b.Property<long>("SifraMesta")
                        .HasColumnType("bigint");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JmbgPacijenta");

                    b.HasIndex("SifraMesta");

                    b.ToTable("Pacijenti");
                });

            modelBuilder.Entity("DataAccess.Entities.TerminTerapije", b =>
                {
                    b.Property<DateTime>("VremeDatumTerapije")
                        .HasColumnType("datetime2");

                    b.Property<long>("RadnikId")
                        .HasColumnType("bigint");

                    b.Property<int>("Kapacitet")
                        .HasColumnType("int");

                    b.Property<long>("SifraUsluge")
                        .HasColumnType("bigint");

                    b.HasKey("VremeDatumTerapije", "RadnikId");

                    b.HasIndex("RadnikId");

                    b.HasIndex("SifraUsluge");

                    b.ToTable("TerminiTerapije");
                });

            modelBuilder.Entity("DataAccess.Entities.UputZaTerapiju", b =>
                {
                    b.Property<string>("BrojUputa")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DatumIzdavanja")
                        .HasColumnType("datetime2");

                    b.Property<string>("JmbgPacijenta")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RedniBrojZahteva")
                        .HasColumnType("int");

                    b.Property<DateTime>("RokVazenja")
                        .HasColumnType("datetime2");

                    b.Property<long>("SifraTerapije")
                        .HasColumnType("bigint");

                    b.Property<long>("SifraUstanove")
                        .HasColumnType("bigint");

                    b.HasKey("BrojUputa");

                    b.HasIndex("JmbgPacijenta")
                        .IsUnique()
                        .HasFilter("[JmbgPacijenta] IS NOT NULL");

                    b.HasIndex("SifraTerapije");

                    b.HasIndex("SifraUstanove");

                    b.ToTable("UputiZaTerapiju");
                });

            modelBuilder.Entity("DataAccess.Entities.Usluga", b =>
                {
                    b.Property<long>("Sifra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sifra");

                    b.ToTable("Usluge");
                });

            modelBuilder.Entity("DataAccess.Entities.Ustanova", b =>
                {
                    b.Property<long>("Sifra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sifra");

                    b.ToTable("Ustanove");
                });

            modelBuilder.Entity("DataAccess.Entities.VrstaTerapije", b =>
                {
                    b.Property<long>("Sifra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sifra");

                    b.ToTable("VrsteTerapija");
                });

            modelBuilder.Entity("DataAccess.Entities.ZdravstvenaKnjizica", b =>
                {
                    b.Property<string>("BrojKnjizice")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("JmbgPacijenta")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrojZdravstvenogOsiguranja")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LBO")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrojKnjizice", "JmbgPacijenta");

                    b.HasIndex("JmbgPacijenta");

                    b.ToTable("ZdravstveneKnjizice");
                });

            modelBuilder.Entity("DataAccess.Entities.ZdravstveniRadnik", b =>
                {
                    b.Property<long>("RadnikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImePrezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JMBG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pol")
                        .HasColumnType("int");

                    b.Property<long>("SifraMesta")
                        .HasColumnType("bigint");

                    b.Property<int>("StepenObrazovanja")
                        .HasColumnType("int");

                    b.HasKey("RadnikID");

                    b.HasIndex("SifraMesta");

                    b.ToTable("ZdravstveniRadnici");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ZdravstveniRadnik");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DataAccess.Entities.Fizioterapeut", b =>
                {
                    b.HasBaseType("DataAccess.Entities.ZdravstveniRadnik");

                    b.HasDiscriminator().HasValue("Fizioterapeut");
                });

            modelBuilder.Entity("DataAccess.Entities.Auth.RefreshToken", b =>
                {
                    b.HasOne("DataAccess.Entities.Auth.ApplicationUser", "User")
                        .WithOne()
                        .HasForeignKey("DataAccess.Entities.Auth.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Entities.EvidencijaTermina", b =>
                {
                    b.HasOne("DataAccess.Entities.KarticaZaEvidenciju", "KarticaZaEvidenciju")
                        .WithMany("ListaTermina")
                        .HasForeignKey("Sifra")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.TerminTerapije", "TerminTerapije")
                        .WithMany()
                        .HasForeignKey("VremeDatumTerapije", "RadnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("KarticaZaEvidenciju");

                    b.Navigation("TerminTerapije");
                });

            modelBuilder.Entity("DataAccess.Entities.KarticaZaEvidenciju", b =>
                {
                    b.HasOne("DataAccess.Entities.UputZaTerapiju", "UputZaTerapiju")
                        .WithOne()
                        .HasForeignKey("DataAccess.Entities.KarticaZaEvidenciju", "BrojUputa");

                    b.HasOne("DataAccess.Entities.Usluga", "Usluga")
                        .WithOne()
                        .HasForeignKey("DataAccess.Entities.KarticaZaEvidenciju", "SifraUsluge")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UputZaTerapiju");

                    b.Navigation("Usluga");
                });

            modelBuilder.Entity("DataAccess.Entities.Pacijent", b =>
                {
                    b.HasOne("DataAccess.Entities.Mesto", "Mesto")
                        .WithMany()
                        .HasForeignKey("SifraMesta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mesto");
                });

            modelBuilder.Entity("DataAccess.Entities.TerminTerapije", b =>
                {
                    b.HasOne("DataAccess.Entities.Fizioterapeut", "Fizioterapeut")
                        .WithMany("TerminiTerapije")
                        .HasForeignKey("RadnikId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Usluga", "Usluga")
                        .WithMany()
                        .HasForeignKey("SifraUsluge")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fizioterapeut");

                    b.Navigation("Usluga");
                });

            modelBuilder.Entity("DataAccess.Entities.UputZaTerapiju", b =>
                {
                    b.HasOne("DataAccess.Entities.Pacijent", "Pacijent")
                        .WithOne()
                        .HasForeignKey("DataAccess.Entities.UputZaTerapiju", "JmbgPacijenta");

                    b.HasOne("DataAccess.Entities.VrstaTerapije", "VrstaTerapije")
                        .WithMany()
                        .HasForeignKey("SifraTerapije")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Ustanova", "Ustanova")
                        .WithMany()
                        .HasForeignKey("SifraUstanove")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacijent");

                    b.Navigation("Ustanova");

                    b.Navigation("VrstaTerapije");
                });

            modelBuilder.Entity("DataAccess.Entities.ZdravstvenaKnjizica", b =>
                {
                    b.HasOne("DataAccess.Entities.Pacijent", "Pacijent")
                        .WithMany("ZdravstveneKnjizice")
                        .HasForeignKey("JmbgPacijenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacijent");
                });

            modelBuilder.Entity("DataAccess.Entities.ZdravstveniRadnik", b =>
                {
                    b.HasOne("DataAccess.Entities.Mesto", "Mesto")
                        .WithMany()
                        .HasForeignKey("SifraMesta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mesto");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DataAccess.Entities.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DataAccess.Entities.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DataAccess.Entities.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Entities.KarticaZaEvidenciju", b =>
                {
                    b.Navigation("ListaTermina");
                });

            modelBuilder.Entity("DataAccess.Entities.Pacijent", b =>
                {
                    b.Navigation("ZdravstveneKnjizice");
                });

            modelBuilder.Entity("DataAccess.Entities.Fizioterapeut", b =>
                {
                    b.Navigation("TerminiTerapije");
                });
#pragma warning restore 612, 618
        }
    }
}