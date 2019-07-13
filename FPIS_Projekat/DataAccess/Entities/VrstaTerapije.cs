using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.Entities
{
    public class VrstaTerapije
    {
        private long _sifra;
        private string naziv;

        public long Sifra { get => _sifra; set => _sifra = value; }
        public string Naziv { get => naziv; set => naziv = value; }
    }
}
