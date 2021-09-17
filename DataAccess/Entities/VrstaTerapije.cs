namespace DataAccess.Entities
{
    public class VrstaTerapije
    {
        private long _sifra;
        private string naziv;

        public long Sifra { get => _sifra; set => _sifra = value; }
        public string Naziv { get => naziv; set => naziv = value; }
    }
}
