using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public interface IAnalizaService
    {
        IList<Analiza> VratiSveAnalize();
        long VratiSifruAnalize();
        Analiza VratiAnalizu(long sifraAnalize);
        void KreirajAnalizu(Analiza analiza);
        void IzmeniAnalizu(Analiza analiza);
        void ObrisiAnalizu(long sifraAnalize);
    }
}
