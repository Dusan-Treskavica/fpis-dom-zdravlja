using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace DataAccess.Entities
{
    public enum Status
    {
        Zakazan = 1,
        Otkazan = 2,
        Izvrsen = 3
    }

    public enum DBStatus
    {
        Insert = 1,
        Update = 2,
        Delete = 3
    }

    public class EvidencijaTermina
    {
        private long _sifra;
        private KarticaZaEvidenciju _karticaZaEvidenciju;
        private long _radnikId;
        private DateTime _vremeDatumTerapije;
        private TerminTerapije _terminTerapije;
        private Status _status;
        private DBStatus _dbStatus;

        public long Sifra { get => _sifra; set => _sifra = value; }
        public KarticaZaEvidenciju KarticaZaEvidenciju { get => _karticaZaEvidenciju; set => _karticaZaEvidenciju = value; }
        public long RadnikId { get => _radnikId; set => _radnikId = value; }
        public DateTime VremeDatumTerapije { get => _vremeDatumTerapije; set => _vremeDatumTerapije = value; }
        public TerminTerapije TerminTerapije { get => _terminTerapije; set => _terminTerapije = value; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get => _status; set => _status = value; }
        public DBStatus DBStatus { get => _dbStatus; set => _dbStatus = value; }


        public override bool Equals(object obj)
        {
            var evidencijaTerminaObj = obj as EvidencijaTermina;
            return evidencijaTerminaObj != null &&
                   _sifra == evidencijaTerminaObj._sifra &&
                   _radnikId == evidencijaTerminaObj._radnikId &&
                   _vremeDatumTerapije == evidencijaTerminaObj._vremeDatumTerapije;
        }

        public bool DaLiJeIstiTermin(TerminTerapije termin)
        {
            return termin != null &&
                   _radnikId == termin.RadnikId &&
                   _vremeDatumTerapije == termin.VremeDatumTerapije;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
