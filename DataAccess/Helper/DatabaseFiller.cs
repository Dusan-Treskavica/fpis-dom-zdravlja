using DataAccess.DBContext;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Helper
{
    public class DatabaseFiller
    {
        public static void PopulateDatabase(FPISDBContext context)
        {
            PopulateUsluge(context);
            PopulateUstanova(context);
            PopulateMesto(context);
            PopulatePacijent(context);
            PopulateZdravstvenaKnjizica(context);
            PopulateZdravstveniRadnici(context);
            PopulateTerminiTerapija(context);
            PopulateVrstaTerapije(context);
            PopulateUputZaTerapiju(context);

            Console.WriteLine("Uspesno sacuvani podaci");
        }

        private static void PopulateUsluge(FPISDBContext context)
        {
            if (context.Usluge.Count() == 0)
                context.Usluge.AddRange(new List<Usluga>
            {
                new Usluga()
                {
                    Naziv = "Masaza"
                },
                new Usluga()
                {
                    Naziv = "Korektivne vezbe"
                },
                new Usluga()
                {
                    Naziv = "Rehabilitacija"
                },
                new Usluga()
                {
                    Naziv = "Kiropraktika"
                }
            }
                );
            context.SaveChanges();
        }

        private static void PopulateUstanova(FPISDBContext context)
        {
            if (context.Ustanove.Count() == 0)
                context.Ustanove.AddRange(new List<Ustanova>
            {
                new Ustanova()
                {
                    Naziv = "Dom zdravlja Novi Beograd"
                },
                new Ustanova()
                {
                    Naziv = "Dom zdravlja Novi Beograd - Medicina rada"
                },
                new Ustanova()
                {
                    Naziv = "Dom zdravlja Zemun"
                }
            }
                );
            context.SaveChanges();
        }

        private static void PopulateMesto(FPISDBContext context)
        {
            if (context.Mesta.Count() == 0)
                context.Mesta.AddRange(new List<Mesto>
            {
                new Mesto()
                {
                    Naziv = "Beograd"
                },
                new Mesto()
                {
                    Naziv = "Novi Sad"
                },
                new Mesto()
                {
                    Naziv = "Kragujevac"
                }
            }
                );
            context.SaveChanges();
        }

        private static void PopulateZdravstveniRadnici(FPISDBContext context)
        {
            if (context.ZdravstveniRadnici.Count() == 0)
                context.ZdravstveniRadnici.AddRange(new List<ZdravstveniRadnik>
            {
                new Fizioterapeut()
                {
                    DatumRodjenja = new DateTime(1990,5,5),
                    ImePrezime = "Marko Markovic",
                    JMBG = "0505990456584",
                    Pol = Pol.Muski,
                    SifraMesta = 1,
                    StepenObrazovanja = StepenObrazovanja.VI
                },
                new Fizioterapeut()
                {
                    DatumRodjenja = new DateTime(1979,7,6),
                    ImePrezime = "Ivana Ivanovic",
                    JMBG = "0607979876532",
                    Pol = Pol.Zenski,
                    SifraMesta = 3,
                    StepenObrazovanja = StepenObrazovanja.VII
                },
                new Fizioterapeut()
                {
                    DatumRodjenja = new DateTime(1986,4,12),
                    ImePrezime = "Sima Simic",
                    JMBG = "1204986456789",
                    Pol = Pol.Muski,
                    SifraMesta = 1,
                    StepenObrazovanja = StepenObrazovanja.VI
                }
            });
            context.SaveChanges();
        }

        private static void PopulateTerminiTerapija(FPISDBContext context)
        {
            if (context.TerminiTerapije.Count() == 0)
                context.TerminiTerapije.AddRange(new List<TerminTerapije>
            {
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,9,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,10,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,11,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,12,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,13,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,14,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,15,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,16,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,9,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,10,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,11,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,12,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,13,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,14,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,15,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,16,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,9,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,10,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,11,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,12,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,13,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,14,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,15,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,2,16,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,9,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,10,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,11,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,12,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,13,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,14,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,15,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,16,0,0),
                    RadnikId =  1,
                    Kapacitet = 6,
                    SifraUsluge = 1
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,9,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,10,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,11,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,12,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,13,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,14,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,15,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,16,0,0),
                    RadnikId =  2,
                    Kapacitet = 6,
                    SifraUsluge = 2
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,9,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,10,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,11,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,12,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,13,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,14,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,15,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                },
                new TerminTerapije()
                {
                    VremeDatumTerapije = new DateTime(2021,10,3,16,0,0),
                    RadnikId =  3,
                    Kapacitet = 5,
                    SifraUsluge = 3
                }
            });
            context.SaveChanges();
        }

        private static void PopulateZdravstvenaKnjizica(FPISDBContext context)
        {
            if (context.ZdravstveneKnjizice.Count() == 0)
                context.ZdravstveneKnjizice.AddRange(new List<ZdravstvenaKnjizica>
            {
                new ZdravstvenaKnjizica()
                {
                    BrojKnjizice = "1",
                    BrojZdravstvenogOsiguranja = "78929",
                    JmbgPacijenta = "0404987468412",
                    LBO = "123456"
                },
                new ZdravstvenaKnjizica()
                {
                    BrojKnjizice = "1",
                    BrojZdravstvenogOsiguranja = "323456",
                    JmbgPacijenta = "1205991789451",
                    LBO = "987654"
                },
                new ZdravstvenaKnjizica()
                {
                    BrojKnjizice = "1",
                    BrojZdravstvenogOsiguranja = "10468",
                    JmbgPacijenta = "1111992365478",
                    LBO = "456123"
                }
            });
            context.SaveChanges();
        }

        private static void PopulateVrstaTerapije(FPISDBContext context)
        {
            if (context.VrsteTerapija.Count() == 0)
                context.VrsteTerapija.AddRange(new List<VrstaTerapije>
            {
                new VrstaTerapije()
                {
                    Naziv = "Terpaija za ledja"
                },
                new VrstaTerapije()
                {
                    Naziv = "Terpaija za kolena"
                },
                new VrstaTerapije()
                {
                    Naziv = "Terapija za vrat"
                }
            }
           );
            context.SaveChanges();
        }

        private static void PopulateUputZaTerapiju(FPISDBContext context)
        {
            if (context.UputiZaTerapiju.Count() == 0)
                context.UputiZaTerapiju.AddRange(new List<UputZaTerapiju>
            {
                new UputZaTerapiju()
                {
                    BrojUputa = "11111111",
                    DatumIzdavanja = new DateTime(2021,10,25),
                    JmbgPacijenta = "0404987468412",
                    RedniBrojZahteva = 1,
                    RokVazenja = new DateTime(2021,11,25),
                    SifraTerapije = 1,
                    SifraUstanove = 1
                },
                new UputZaTerapiju()
                {
                    BrojUputa = "22222222",
                    DatumIzdavanja = new DateTime(2021,10,25),
                    JmbgPacijenta = "1205991789451",
                    RedniBrojZahteva = 1,
                    RokVazenja = new DateTime(2021,10,30),
                    SifraTerapije = 2,
                    SifraUstanove = 1
                },
                new UputZaTerapiju()
                {
                    BrojUputa = "33333333",
                    DatumIzdavanja = new DateTime(2021,10,25),
                    JmbgPacijenta = "1111992365478",
                    RedniBrojZahteva = 2,
                    RokVazenja = new DateTime(2021,11,25),
                    SifraTerapije = 1,
                    SifraUstanove = 2
                }
            }
           );
            context.SaveChanges();
        }

        private static void PopulatePacijent(FPISDBContext context)
        {
            if (context.Pacijenti.Count() == 0)
                context.Pacijenti.AddRange(new List<Pacijent>
            {
                new Pacijent()
                {
                    JmbgPacijenta = "0404987468412",
                    ImePrezime = "Rade Radic",
                    DatumRodjenja = new DateTime(1987,4,4),
                    SifraMesta = 1,
                    Pol = Pol.Muski,
                    Telefon = "0612345678"
                },
                new Pacijent()
                {
                    JmbgPacijenta = "1205991789451",
                    ImePrezime = "Misa Misic",
                    DatumRodjenja = new DateTime(1991,5,12),
                    SifraMesta = 2,
                    Pol = Pol.Muski,
                    Telefon = "0648973254"
                },
                new Pacijent()
                {
                    JmbgPacijenta = "1111992365478",
                    ImePrezime = "Nikola Nikolic",
                    DatumRodjenja = new DateTime(1992,11,11),
                    SifraMesta = 1,
                    Pol = Pol.Muski,
                    Telefon = "063452378"
                }
            }
           );
            context.SaveChanges();
        }
    }
}
