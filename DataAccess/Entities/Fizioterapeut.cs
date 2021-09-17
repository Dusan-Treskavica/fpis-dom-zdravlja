using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Fizioterapeut : ZdravstveniRadnik
    {
        private IList<TerminTerapije> _terminiTerapije;

        public IList<TerminTerapije> TerminiTerapije { get => _terminiTerapije; set => _terminiTerapije = value; }
    }
}
