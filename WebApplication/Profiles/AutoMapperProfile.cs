using AutoMapper;
using DataAccess.Entities;
using System;
using WebApplication.DTO;

namespace WebApplication.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TerminTerapije, TerminTerapijeDTO>()
                .ForMember(dest => dest.Fizioterapeut, opt => opt.MapFrom(src => src.Fizioterapeut.ImePrezime))
                .ForMember(dest => dest.NazivUsluge, opt => opt.MapFrom(src => src.Usluga.Naziv));

            CreateMap<TerminTerapijeDTO, TerminTerapije>()
                .ForMember(dest => dest.Fizioterapeut, opt => opt.Ignore());

            CreateMap<Usluga, UslugaDTO>()
                .ForMember(dest => dest.Sifra, opt => opt.MapFrom(src => src.Sifra))
                .ForMember(dest => dest.Naziv, opt => opt.MapFrom(src => src.Naziv))
                .ReverseMap();

            CreateMap<UputZaTerapiju, UputZaTerapijuDTO>();
            CreateMap<UputZaTerapijuDTO, UputZaTerapiju>()
                .ForMember(dest => dest.RedniBrojZahteva, opt => opt.Ignore())
                .ForMember(dest => dest.RokVazenja, opt => opt.Ignore())
                .ForMember(dest => dest.SifraTerapije, opt => opt.Ignore())
                .ForMember(dest => dest.SifraUstanove, opt => opt.Ignore())
                .ForMember(dest => dest.VrstaTerapije, opt => opt.Ignore())
                .ForMember(dest => dest.Pacijent, opt => opt.Ignore())
                .AfterMap((src, dest) => 
                {
                    dest.Pacijent = null;
                });

            CreateMap<Pacijent, PacijentDTO>()
                .ForMember(dest => dest.DatumRodjenja, opt => opt.MapFrom(src => src.DatumRodjenja.ToShortDateString()));

            CreateMap<PacijentDTO, Pacijent>()
                .ForMember(dest => dest.DatumRodjenja, opt => opt.MapFrom(src => Convert.ToDateTime(src.DatumRodjenja)));

            CreateMap<EvidencijaTermina, EvidencijaTerminaDTO>()
                .ForMember(dest => dest.SifraKartice, opt => opt.MapFrom(src => src.Sifra))
                .ForMember(dest => dest.TerminTerapije, opt => opt.MapFrom(src => src.TerminTerapije))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<EvidencijaTerminaDTO, EvidencijaTermina>()
                .ForMember(dest => dest.Sifra, opt => opt.MapFrom(src => src.SifraKartice))
                .ForMember(dest => dest.RadnikId, opt => opt.MapFrom(src => src.TerminTerapije.RadnikId))
                .ForMember(dest => dest.VremeDatumTerapije, opt => opt.MapFrom(src => src.TerminTerapije.VremeDatumTerapije))
                .ForMember(dest => dest.TerminTerapije, opt => opt.MapFrom(src => src.TerminTerapije))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<Analiza, AnalizaDTO>();

            CreateMap<KarticaZaEvidenciju, KarticaZaEvidencijuDTO>()
               .ForMember(dest => dest.NazivUsluge, opt => opt.MapFrom(src => src.Usluga.Naziv))
               .ForMember(dest => dest.OdabraniTermini, opt => opt.MapFrom(src => src.ListaTermina));

            CreateMap<KarticaZaEvidencijuDTO, KarticaZaEvidenciju>()
                .ForMember(dest => dest.Usluga, opt => opt.Ignore())
                .ForMember(dest => dest.DBStatus, opt => opt.Ignore())
                .ForMember(dest => dest.ListaTermina, opt => opt.MapFrom(src => src.OdabraniTermini));
        }
    }
}
