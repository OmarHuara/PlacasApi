using AutoMapper;
using PlacasAPI.Dtos;
using PlacasAPI.Models;

namespace PlacasAPI.Mappings
{
    public class AutomovelMapping : Profile
    {
        public AutomovelMapping()
        {
            CreateMap(typeof(ResponseGeneric<>), typeof(ResponseGeneric<>));
            CreateMap<AutomovelDto, Automovel>();
            CreateMap<Automovel, AutomovelDto>()
                .ForMember(dest => dest.marca, opt => opt.MapFrom(src => src.brand))
                .ForMember(dest => dest.modelo, opt => opt.MapFrom(src => src.model))
                .ForMember(dest => dest.importado, opt => opt.MapFrom(src => src.imported))
                .ForMember(dest => dest.ano, opt => opt.MapFrom(src => src.year))
                .ForMember(dest => dest.cor, opt => opt.MapFrom(src => src.color))
                .ForMember(dest => dest.cilindrada, opt => opt.MapFrom(src => src.displacement))
                .ForMember(dest => dest.potencia, opt => opt.MapFrom(src => src.power))
                .ForMember(dest => dest.combustivel, opt => opt.MapFrom(src => src.fuel))
                .ForMember(dest => dest.chassi, opt => opt.MapFrom(src => src.chassis))
                .ForMember(dest => dest.uf, opt => opt.MapFrom(src => src.uf))
                .ForMember(dest => dest.municipio, opt => opt.MapFrom(src => src.Municipality))
                .ForMember(dest => dest.segmento, opt => opt.MapFrom(src => src.segment))
                .ForMember(dest => dest.especieVeiculo, opt => opt.MapFrom(src => src.VehicleType));

            CreateMap<List<string>, Automovel>()
                .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src[0]))
                .ForMember(dest => dest.model, opt => opt.MapFrom(src => src[1]))
                .ForMember(dest => dest.imported, opt => opt.MapFrom(src => src[2]))
                .ForMember(dest => dest.year, opt => opt.MapFrom(src => src[3]))
                .ForMember(dest => dest.color, opt => opt.MapFrom(src => src[4]))
                .ForMember(dest => dest.displacement, opt => opt.MapFrom(src => src[5]))
                .ForMember(dest => dest.power, opt => opt.MapFrom(src => src[6]))
                .ForMember(dest => dest.fuel, opt => opt.MapFrom(src => src[7]))
                .ForMember(dest => dest.chassis, opt => opt.MapFrom(src => src[8]))
                .ForMember(dest => dest.uf, opt => opt.MapFrom(src => src[9]))
                .ForMember(dest => dest.Municipality, opt => opt.MapFrom(src => src[10]))
                .ForMember(dest => dest.segment, opt => opt.MapFrom(src => src[11]))
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => string.IsNullOrEmpty(src[12]) ? src[12] : ""));

        }
    }
}
