using System.ComponentModel;

namespace DataAccess.Entities
{
    public enum JedinicaMere
    {
        [Description("")]
        Empty,
        [Description("mg")]
        Miligram,
        [Description("%")]
        Procenat,
        [Description("1/l")]
        JedinicaPoLitry,
        [Description("g/l")]
        gramiPoLitru,
    }

    public class Analiza
    {
        public long SifraAnalize { get; set; }
        public string NazivAnalize { get; set; }
        public double DonjaGranica { get; set; }
        public JedinicaMere DonjaGranicaJedinicaMere { get; set; }
        public double GornjaGranica { get; set; }
        public JedinicaMere GornjaGranicaJedinicaMere { get; set; }
    }
}
