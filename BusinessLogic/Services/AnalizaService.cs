using Common;
using Common.Exceptions;
using Common.Util;
using DataAccess.DB;
using DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Services
{
    public class AnalizaService : IAnalizaService
    {
        private readonly IDBBroker dbBroker;
        private readonly IHttpContextUtil httpContextUtil;

        public AnalizaService(IDBBroker dbBroker, IHttpContextUtil httpContextUtil)
        {
            this.dbBroker = dbBroker;
            this.httpContextUtil = httpContextUtil;
        }

        public Analiza VratiAnalizu(long sifraAnalize)
        {
            var analiza = dbBroker.VratiAnalizu(sifraAnalize);
            if (analiza == null)
            {
                throw new RequestProcessingException("Ne postoji trazena analiza !!!");
            }
            else
            {
                this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_ANALIZA_OBJECT, analiza);
            }

            return analiza;
        }

        public long VratiSifruAnalize()
        {
            long sifra = dbBroker.VratiSifruAnalize();
               
            Analiza novaAnaliza = new Analiza { SifraAnalize = sifra };
            httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_ANALIZA_OBJECT, novaAnaliza);

            return sifra;
        }

        public IList<Analiza> VratiSveAnalize()
        {
            return this.dbBroker.VratiSveAnalize();
        }

        public void KreirajAnalizu(Analiza analiza)
        {
            if (analiza.DonjaGranica > analiza.GornjaGranica)
            {
                throw new RequestProcessingException("Vrednost donje granice mora biti manja od vrednosti gornje granice analize !!!");
            }
            if (analiza.DonjaGranicaJedinicaMere != analiza.GornjaGranicaJedinicaMere)
            {
                throw new RequestProcessingException("Jedinice mera nisu iste za gornju i donju granicu analize !!!");
            }
            Analiza contextAnaliza = this.httpContextUtil.GetFromSession<Analiza>(ApplicationConsts.HTTP_CONTEXT_ANALIZA_OBJECT);


            contextAnaliza.NazivAnalize = analiza.NazivAnalize;
            contextAnaliza.DonjaGranica = analiza.DonjaGranica;
            contextAnaliza.GornjaGranica = analiza.GornjaGranica;
            contextAnaliza.DonjaGranicaJedinicaMere = analiza.DonjaGranicaJedinicaMere;
            contextAnaliza.GornjaGranicaJedinicaMere = analiza.GornjaGranicaJedinicaMere;

            this.dbBroker.ZapamtiAnalizu(contextAnaliza);
            this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_ANALIZA_OBJECT, contextAnaliza);
        }

        public void IzmeniAnalizu(Analiza analiza)
        {
            
            if (analiza.DonjaGranica > analiza.GornjaGranica)
            {
                throw new RequestProcessingException("Vrednost donje granice mora biti manja od vrednosti gornje granice analize !!!");
            }
            if (analiza.DonjaGranicaJedinicaMere != analiza.GornjaGranicaJedinicaMere)
            {
                throw new RequestProcessingException("Jedinica mere za gornju i donju vrednost analize mora biti ista  !!!");
            }
            Analiza contextAnaliza = this.httpContextUtil.GetFromSession<Analiza>(ApplicationConsts.HTTP_CONTEXT_ANALIZA_OBJECT);

            contextAnaliza.NazivAnalize = analiza.NazivAnalize;
            contextAnaliza.DonjaGranica = analiza.DonjaGranica;
            contextAnaliza.GornjaGranica = analiza.GornjaGranica;
            contextAnaliza.DonjaGranicaJedinicaMere = analiza.DonjaGranicaJedinicaMere;
            contextAnaliza.GornjaGranicaJedinicaMere = analiza.GornjaGranicaJedinicaMere;

            this.dbBroker.IzmeniAnalizu(contextAnaliza);
            this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_ANALIZA_OBJECT, contextAnaliza);
            
        }

        public void ObrisiAnalizu(long sifraAnalize)
        {
            if(dbBroker.VratiAnalizu(sifraAnalize) == null)
            {
                throw new RequestProcessingException("Nije moguce obrisati analizu. Izabrana analiza ne postoji");
            }
            this.dbBroker.ObrisiAnalizu(sifraAnalize);
        }
    }
}
