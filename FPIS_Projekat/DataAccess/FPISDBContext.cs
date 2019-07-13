using FPIS_Projekat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess
{
    public class FPISDBContext : DbContext
    {
        public FPISDBContext(DbContextOptions<FPISDBContext> options)
           : base(options)
        {
        }

        public DbSet<Analiza> Analize { get; set; }
        public DbSet<EvidencijaTermina> EvidencijaTermina { get; set; }
        public DbSet<Fizioterapeut> Fizioterapeuti { get; set; }
        public DbSet<KarticaZaEvidenciju> KarticeZaEvidenciju { get; set; }
        public DbSet<Mesto> Mesta { get; set; }
        public DbSet<Pacijent> Pacijenti { get; set; }
        public DbSet<TerminTerapije> TerminiTerapije { get; set; }
        public DbSet<UputZaTerapiju> UputiZaTerapiju { get; set; }
        public DbSet<Usluga> Usluge { get; set; }
        public DbSet<Ustanova> Ustanove { get; set; }
        public DbSet<VrstaTerapije> VrsteTerapija { get; set; }
        public DbSet<ZdravstvenaKnjizica> ZdravstveneKnjizice { get; set; }
        public DbSet<ZdravstveniRadnik> ZdravstveniRadnici { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analiza>()
                .HasKey(a => a.SifraAnalize);

            modelBuilder.Entity<Pacijent>()
                .HasKey(p => p.JmbgPacijenta);
           
            modelBuilder.Entity<ZdravstvenaKnjizica>()
                .HasKey(zk => new { zk.BrojKnjizice, zk.JmbgPacijenta});

            modelBuilder.Entity<UputZaTerapiju>()
                .HasKey(ut => ut.BrojUputa);

            modelBuilder.Entity<VrstaTerapije>()
                .HasKey(vt => vt.Sifra);

            modelBuilder.Entity<Ustanova>()
                .HasKey(u => u.Sifra);

            modelBuilder.Entity<KarticaZaEvidenciju>()
                .HasKey(kze => kze.SifraKartice);

            modelBuilder.Entity<ZdravstveniRadnik>()
                .HasKey(zr => zr.RadnikID);

            modelBuilder.Entity<Fizioterapeut>().HasBaseType<ZdravstveniRadnik>();
            
            modelBuilder.Entity<Mesto>()
               .HasKey(m => m.Sifra);

            modelBuilder.Entity<Usluga>()
               .HasKey(u => u.Sifra);

            modelBuilder.Entity<TerminTerapije>()
                .HasKey(tt => new { tt.VremeDatumTerapije, tt.RadnikId });

            modelBuilder.Entity<EvidencijaTermina>()
                .HasKey(et => new { et.Sifra, et.VremeDatumTerapije, et.RadnikId });

            modelBuilder.Entity<EvidencijaTermina>()
                .Ignore(et => et.DBStatus);

            #region Pacijent-ZdravstvenaKnjizica-Mesto

            modelBuilder.Entity<Pacijent>()
                .HasMany<ZdravstvenaKnjizica>(p => p.ZdravstveneKnjizice)
                .WithOne(zk => zk.Pacijent)
                .HasForeignKey(zk => zk.JmbgPacijenta)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ZdravstvenaKnjizica>()
                .HasOne<Pacijent>(zk => zk.Pacijent)
                .WithMany(p => p.ZdravstveneKnjizice)
                .HasForeignKey(zk => zk.JmbgPacijenta);

            modelBuilder.Entity<Pacijent>()
                .HasOne<Mesto>(p => p.Mesto)
                .WithMany()
                .HasForeignKey(p => p.SifraMesta);

            #endregion

            #region UputZaTerpaiju-VrstaTerapije-Ustanova-Pacijent

            modelBuilder.Entity<UputZaTerapiju>()
               .HasOne<VrstaTerapije>(ut => ut.VrstaTerapije)
               .WithMany()
               .HasForeignKey(ut => ut.SifraTerapije);

            modelBuilder.Entity<UputZaTerapiju>()
                .HasOne<Ustanova>(ut => ut.Ustanova)
                .WithMany()
                .HasForeignKey(ut => ut.SifraUstanove);

            modelBuilder.Entity<UputZaTerapiju>()
                .HasOne<Pacijent>(ut => ut.Pacijent)
                .WithOne()
                .HasForeignKey<UputZaTerapiju>(ut => ut.JmbgPacijenta);

            #endregion

            #region KartizaZaEvidenciju-Usluga-UputZaTerapiju

            modelBuilder.Entity<KarticaZaEvidenciju>()
                .HasOne(kze => kze.Usluga)
                .WithOne()
                .HasForeignKey<KarticaZaEvidenciju>(kze => kze.SifraUsluge);

            modelBuilder.Entity<KarticaZaEvidenciju>()
                .HasOne(kze => kze.UputZaTerapiju)
                .WithOne()
                .HasForeignKey<KarticaZaEvidenciju>(kze => kze.BrojUputa);

            #endregion

            #region ZdravstveniRadnik-Mesto

            modelBuilder.Entity<ZdravstveniRadnik>()
                .HasOne<Mesto>(zr => zr.Mesto)
                .WithMany()
                .HasForeignKey(zr => zr.SifraMesta);

            #endregion

            #region Fizioterapeut-TerminTerapije

            modelBuilder.Entity<Fizioterapeut>()
                .HasMany<TerminTerapije>(f => f.TerminiTerapije)
                .WithOne(tt => tt.Fizioterapeut)
                .HasForeignKey(tt => tt.RadnikId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TerminTerapije>()
                .HasOne<Fizioterapeut>(tt => tt.Fizioterapeut)
                .WithMany(f => f.TerminiTerapije)
                .HasForeignKey(tt => tt.RadnikId);

            #endregion

            #region TerminTrapije-Usluga

            modelBuilder.Entity<TerminTerapije>()
                .HasOne<Usluga>(tt => tt.Usluga)
                .WithMany()
                .HasForeignKey(tt => tt.SifraUsluge);

            #endregion

            #region EvidencijaTermina

            modelBuilder.Entity<EvidencijaTermina>()
                .HasOne<KarticaZaEvidenciju>(et => et.KarticaZaEvidenciju)
                .WithMany(kze => kze.ListaTermina)
                .HasForeignKey(et => et.Sifra)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EvidencijaTermina>()
                .HasOne<TerminTerapije>(et => et.TerminTerapije)
                .WithMany()
                .HasForeignKey(et => new { et.VremeDatumTerapije, et.RadnikId})
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

        }
    }
}
