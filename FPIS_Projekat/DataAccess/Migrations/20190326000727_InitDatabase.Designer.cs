﻿// <auto-generated />
using FPIS_Projekat.DataAccess;
using FPIS_Projekat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace FPIS_Projekat.DataAccess.Migrations
{
    [DbContext(typeof(FPISDBContext))]
    [Migration("20190326000727_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.Analiza", b =>
                {
                    b.Property<long>("SifraAnalize")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("DonjaGranica");

                    b.Property<int>("DonjaGranicaJedinicaMere");

                    b.Property<double>("GornjaGranica");

                    b.Property<int>("GornjaGranicaJedinicaMere");

                    b.Property<string>("NazivAnalize");

                    b.HasKey("SifraAnalize");

                    b.ToTable("Analize");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.EvidencijaTermina", b =>
                {
                    b.Property<long>("Sifra");

                    b.Property<DateTime>("VremeDatumTerapije");

                    b.Property<long>("RadnikId");

                    b.Property<int>("Status");

                    b.HasKey("Sifra", "VremeDatumTerapije", "RadnikId");

                    b.HasIndex("VremeDatumTerapije", "RadnikId");

                    b.ToTable("EvidencijaTermina");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.KarticaZaEvidenciju", b =>
                {
                    b.Property<long>("SifraKartice")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrojUputa");

                    b.Property<DateTime>("DatumIzdavanja");

                    b.Property<long>("SifraUsluge");

                    b.HasKey("SifraKartice");

                    b.HasIndex("BrojUputa")
                        .IsUnique()
                        .HasFilter("[BrojUputa] IS NOT NULL");

                    b.HasIndex("SifraUsluge")
                        .IsUnique();

                    b.ToTable("KarticeZaEvidenciju");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.Mesto", b =>
                {
                    b.Property<long>("Sifra")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("Sifra");

                    b.ToTable("Mesta");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.Pacijent", b =>
                {
                    b.Property<string>("JmbgPacijenta")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<string>("ImePrezime");

                    b.Property<int>("Pol");

                    b.Property<long>("SifraMesta");

                    b.Property<string>("Telefon");

                    b.HasKey("JmbgPacijenta");

                    b.HasIndex("SifraMesta");

                    b.ToTable("Pacijenti");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.TerminTerapije", b =>
                {
                    b.Property<DateTime>("VremeDatumTerapije");

                    b.Property<long>("RadnikId");

                    b.Property<int>("Kapacitet");

                    b.Property<long>("SifraUsluge");

                    b.HasKey("VremeDatumTerapije", "RadnikId");

                    b.HasIndex("RadnikId");

                    b.HasIndex("SifraUsluge");

                    b.ToTable("TerminiTerapije");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.UputZaTerapiju", b =>
                {
                    b.Property<string>("BrojUputa")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumIzdavanja");

                    b.Property<string>("JmbgPacijenta");

                    b.Property<int>("RedniBrojZahteva");

                    b.Property<DateTime>("RokVazenja");

                    b.Property<long>("SifraTerapije");

                    b.Property<long>("SifraUstanove");

                    b.HasKey("BrojUputa");

                    b.HasIndex("JmbgPacijenta")
                        .IsUnique()
                        .HasFilter("[JmbgPacijenta] IS NOT NULL");

                    b.HasIndex("SifraTerapije");

                    b.HasIndex("SifraUstanove");

                    b.ToTable("UputiZaTerapiju");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.Usluga", b =>
                {
                    b.Property<long>("Sifra")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("Sifra");

                    b.ToTable("Usluge");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.Ustanova", b =>
                {
                    b.Property<long>("Sifra")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("Sifra");

                    b.ToTable("Ustanove");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.VrstaTerapije", b =>
                {
                    b.Property<long>("Sifra")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Naziv");

                    b.HasKey("Sifra");

                    b.ToTable("VrsteTerapija");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.ZdravstvenaKnjizica", b =>
                {
                    b.Property<string>("BrojKnjizice");

                    b.Property<string>("JmbgPacijenta");

                    b.Property<string>("BrojZdravstvenogOsiguranja");

                    b.Property<string>("LBO");

                    b.HasKey("BrojKnjizice", "JmbgPacijenta");

                    b.HasIndex("JmbgPacijenta");

                    b.ToTable("ZdravstveneKnjizice");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.ZdravstveniRadnik", b =>
                {
                    b.Property<long>("RadnikID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("ImePrezime");

                    b.Property<string>("JMBG");

                    b.Property<int>("Pol");

                    b.Property<long>("SifraMesta");

                    b.Property<int>("StepenObrazovanja");

                    b.HasKey("RadnikID");

                    b.HasIndex("SifraMesta");

                    b.ToTable("ZdravstveniRadnici");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ZdravstveniRadnik");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.Fizioterapeut", b =>
                {
                    b.HasBaseType("FPIS_Projekat.DataAccess.Entities.ZdravstveniRadnik");


                    b.ToTable("Fizioterapeut");

                    b.HasDiscriminator().HasValue("Fizioterapeut");
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.EvidencijaTermina", b =>
                {
                    b.HasOne("FPIS_Projekat.DataAccess.Entities.KarticaZaEvidenciju", "KarticaZaEvidenciju")
                        .WithMany("ListaTermina")
                        .HasForeignKey("Sifra")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FPIS_Projekat.DataAccess.Entities.TerminTerapije", "TerminTerapije")
                        .WithMany()
                        .HasForeignKey("VremeDatumTerapije", "RadnikId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.KarticaZaEvidenciju", b =>
                {
                    b.HasOne("FPIS_Projekat.DataAccess.Entities.UputZaTerapiju", "UputZaTerapiju")
                        .WithOne()
                        .HasForeignKey("FPIS_Projekat.DataAccess.Entities.KarticaZaEvidenciju", "BrojUputa");

                    b.HasOne("FPIS_Projekat.DataAccess.Entities.Usluga", "Usluga")
                        .WithOne()
                        .HasForeignKey("FPIS_Projekat.DataAccess.Entities.KarticaZaEvidenciju", "SifraUsluge")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.Pacijent", b =>
                {
                    b.HasOne("FPIS_Projekat.DataAccess.Entities.Mesto", "Mesto")
                        .WithMany()
                        .HasForeignKey("SifraMesta")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.TerminTerapije", b =>
                {
                    b.HasOne("FPIS_Projekat.DataAccess.Entities.Fizioterapeut", "Fizioterapeut")
                        .WithMany("TerminiTerapije")
                        .HasForeignKey("RadnikId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FPIS_Projekat.DataAccess.Entities.Usluga", "Usluga")
                        .WithMany()
                        .HasForeignKey("SifraUsluge")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.UputZaTerapiju", b =>
                {
                    b.HasOne("FPIS_Projekat.DataAccess.Entities.Pacijent", "Pacijent")
                        .WithOne()
                        .HasForeignKey("FPIS_Projekat.DataAccess.Entities.UputZaTerapiju", "JmbgPacijenta");

                    b.HasOne("FPIS_Projekat.DataAccess.Entities.VrstaTerapije", "VrstaTerapije")
                        .WithMany()
                        .HasForeignKey("SifraTerapije")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FPIS_Projekat.DataAccess.Entities.Ustanova", "Ustanova")
                        .WithMany()
                        .HasForeignKey("SifraUstanove")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.ZdravstvenaKnjizica", b =>
                {
                    b.HasOne("FPIS_Projekat.DataAccess.Entities.Pacijent", "Pacijent")
                        .WithMany("ZdravstveneKnjizice")
                        .HasForeignKey("JmbgPacijenta")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FPIS_Projekat.DataAccess.Entities.ZdravstveniRadnik", b =>
                {
                    b.HasOne("FPIS_Projekat.DataAccess.Entities.Mesto", "Mesto")
                        .WithMany()
                        .HasForeignKey("SifraMesta")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
