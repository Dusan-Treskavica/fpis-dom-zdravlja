using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.Entities
{
    public enum StepenObrazovanja
    {
        IV,
        V,
        VI,
        VII
    }

    public class ZdravstveniRadnik
    {
        private long _radnikID;
        private string _imePrezime;
        private DateTime _datumRodjenja;
        private Pol _pol;
        private string _jmbg;
        private StepenObrazovanja _stepenObrazovanja;
        private long _sifraMesta;
        private Mesto _mesto;

        public long RadnikID { get => _radnikID; set => _radnikID = value; }
        public string ImePrezime { get => _imePrezime; set => _imePrezime = value; }
        public DateTime DatumRodjenja { get => _datumRodjenja; set => _datumRodjenja = value; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Pol Pol { get => _pol; set => _pol = value; }
        public string JMBG { get => _jmbg; set => _jmbg = value; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StepenObrazovanja StepenObrazovanja { get => _stepenObrazovanja; set => _stepenObrazovanja = value; }
        public long SifraMesta { get => _sifraMesta; set => _sifraMesta = value; }
        public Mesto Mesto { get => _mesto; set => _mesto = value; }
    }
}
