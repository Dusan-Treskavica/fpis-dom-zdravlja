using AutoMapper;
using FPIS_Projekat.DataAccess.Entities;
using FPIS_Projekat.Services.DTO;

namespace FPIS_Projekat.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TerminTerapije, TerminTerapijeDTO>()
                .ForMember(dest => dest.Fizioterapeut, opt => opt.MapFrom(src => src.Fizioterapeut.ImePrezime))
                .ForMember(dest => dest.Usluga, opt => opt.MapFrom(src => src.Usluga.Naziv))
                .ReverseMap();

            CreateMap<Usluga, UslugaDTO>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Sifra))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Naziv))
                .ReverseMap();

            CreateMap<KarticaZaEvidenciju, KarticaZaEvidencijuDTO>()
                .ForMember(dest => dest.NazivUsluge, opt => opt.MapFrom(src => src.Usluga.Naziv))
                .ForMember(dest => dest.OdabraniTermini, opt => opt.MapFrom(src => src.ListaTermina))
                .ReverseMap();

            CreateMap<EvidencijaTermina, EvidencijaTerminaDTO>()
                .ForMember(dest => dest.SifraKartice, opt => opt.MapFrom(src => src.Sifra))
                .ForMember(dest => dest.ImeRadnika, opt => opt.MapFrom(src => src.TerminTerapije.Fizioterapeut.ImePrezime))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ReverseMap();


            CreateMap<Analiza, AnalizaDTO>();

            CreateMap<UputZaTerapiju, UputZaTerapijuDTO>();

            CreateMap<Pacijent, PacijentDTO>();
        }
    }
}
