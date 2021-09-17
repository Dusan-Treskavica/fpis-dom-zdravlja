using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public enum Pol
    {
        Muski,
        Zenski
    }

    public class Pacijent
    {
        private string _jmbgPacijenta;
        private string _imePrezime;
        private DateTime _datumRodjenja;
        private Pol _pol;
        private string _telefon;
        private long _sifraMesta;
        private Mesto _mesto;
        private ICollection<ZdravstvenaKnjizica> _zdravstveneKnjizice;

        public string JmbgPacijenta { get => _jmbgPacijenta; set => _jmbgPacijenta = value; }
        public string ImePrezime { get => _imePrezime; set => _imePrezime = value; }
        public DateTime DatumRodjenja { get => _datumRodjenja; set => _datumRodjenja = value; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Pol Pol { get => _pol; set => _pol = value; }
        public string Telefon { get => _telefon; set => _telefon = value; }
        public long SifraMesta { get => _sifraMesta; set => _sifraMesta = value; }
        public Mesto Mesto { get => _mesto; set => _mesto = value; }
        public ICollection<ZdravstvenaKnjizica> ZdravstveneKnjizice { get => _zdravstveneKnjizice; set => _zdravstveneKnjizice = value; }
    }
}
