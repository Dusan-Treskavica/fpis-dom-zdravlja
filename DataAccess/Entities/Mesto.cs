namespace DataAccess.Entities
{
    public class Mesto
    {
        private long _sifra;
        private string _naziv;

        public long Sifra { get => _sifra; set => _sifra = value; }
        public string Naziv { get => _naziv; set => _naziv = value; }
    }
}