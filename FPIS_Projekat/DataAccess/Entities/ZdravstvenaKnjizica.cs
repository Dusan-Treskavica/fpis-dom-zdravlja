using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.Entities
{
    public class ZdravstvenaKnjizica
    {
        private string _brojKnjizice;
        private string _brojZdravstvenogOsiguranja;
        private string _lbo;
        private string _jmbgPacijenta;
        private Pacijent _pacijent;

        public string BrojKnjizice { get => _brojKnjizice; set => _brojKnjizice = value; }
        public string BrojZdravstvenogOsiguranja { get => _brojZdravstvenogOsiguranja; set => _brojZdravstvenogOsiguranja = value; }
        public string LBO { get => _lbo; set => _lbo = value; }
        public string JmbgPacijenta { get => _jmbgPacijenta; set => _jmbgPacijenta = value; }
        public Pacijent Pacijent { get => _pacijent; set => _pacijent = value; }
    }
}
