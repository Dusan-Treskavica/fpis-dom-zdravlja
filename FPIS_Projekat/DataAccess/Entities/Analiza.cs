using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.Entities
{
    public enum JedinicaMere
    {
        [Description("")]
        Empty,

        [Description("mol")]
        Mol,

        [Description("mmol")]
        MoliMol,

        [Description("mol/mm2")]
        MolPoKvadratnomMilimetru,

        [Description("g/L")]
        gramiPoLitru,
    }

    public class Analiza
    {
        private long _sifraAnalize;
        private string _nazivAnalize;
        private double _donjaGranica;
        private JedinicaMere _donjaGranicaJedinicaMere;
        private double _gornjaGranica;
        private JedinicaMere _gornjaGranicaJedinicaMere;

        public long SifraAnalize { get => _sifraAnalize; set => _sifraAnalize = value; }
        public string NazivAnalize { get => _nazivAnalize; set => _nazivAnalize = value; }
        public double DonjaGranica { get => _donjaGranica; set => _donjaGranica = value; }
        public JedinicaMere DonjaGranicaJedinicaMere { get => _donjaGranicaJedinicaMere; set => _donjaGranicaJedinicaMere = value; }
        public double GornjaGranica { get => _gornjaGranica; set => _gornjaGranica = value; }
        public JedinicaMere GornjaGranicaJedinicaMere { get => _gornjaGranicaJedinicaMere; set => _gornjaGranicaJedinicaMere = value; }
    }
}
