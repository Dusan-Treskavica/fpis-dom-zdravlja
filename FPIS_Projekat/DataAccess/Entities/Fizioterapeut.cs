using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.Entities
{
    public class Fizioterapeut : ZdravstveniRadnik
    {
        private IList<TerminTerapije> _terminiTerapije;

        public IList<TerminTerapije> TerminiTerapije { get => _terminiTerapije; set => _terminiTerapije = value; }
    }
}
