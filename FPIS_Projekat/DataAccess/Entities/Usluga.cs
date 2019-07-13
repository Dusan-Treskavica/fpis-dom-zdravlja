using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.Entities
{
    public class Usluga
    {
        private long _sifra;
        private string _naziv;

        public long Sifra { get => _sifra; set => _sifra = value; }
        public string Naziv { get => _naziv; set => _naziv = value; }
    }
}
