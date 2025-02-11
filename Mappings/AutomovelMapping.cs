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
            CreateMap<AutomovelDto, Automovel>()
                .ForMember(dest => dest.plate, opt => opt.MapFrom(src => src.placa))
                .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src.marca))
                .ForMember(dest => dest.model, opt => opt.MapFrom(src => src.modelo))
                .ForMember(dest => dest.imported, opt => opt.MapFrom(src => src.importado))
                .ForMember(dest => dest.year, opt => opt.MapFrom(src => src.ano))
                .ForMember(dest => dest.modelYear, opt => opt.MapFrom(src => src.AnoModelo))
                .ForMember(dest => dest.color, opt => opt.MapFrom(src => src.cor))
                .ForMember(dest => dest.displacement, opt => opt.MapFrom(src => src.cilindrada))
                .ForMember(dest => dest.power, opt => opt.MapFrom(src => src.potencia))
                .ForMember(dest => dest.fuel, opt => opt.MapFrom(src => src.combustivel))
                .ForMember(dest => dest.chassis, opt => opt.MapFrom(src => src.chassi))
                .ForMember(dest => dest.uf, opt => opt.MapFrom(src => src.uf))
                .ForMember(dest => dest.Municipality, opt => opt.MapFrom(src => src.municipio))
                .ForMember(dest => dest.segment, opt => opt.MapFrom(src => src.segmento))
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.especieVeiculo));

            CreateMap<Automovel, AutomovelDto>()
                .ForMember(dest => dest.placa, opt => opt.MapFrom(src => src.plate))
                .ForMember(dest => dest.marca, opt => opt.MapFrom(src => src.brand))
                .ForMember(dest => dest.modelo, opt => opt.MapFrom(src => src.model))
                .ForMember(dest => dest.importado, opt => opt.MapFrom(src => src.imported))
                .ForMember(dest => dest.ano, opt => opt.MapFrom(src => src.modelYear))
                .ForMember(dest => dest.AnoModelo, opt => opt.MapFrom(src => src.year))
                .ForMember(dest => dest.cor, opt => opt.MapFrom(src => src.color))
                .ForMember(dest => dest.cilindrada, opt => opt.MapFrom(src => src.displacement))
                .ForMember(dest => dest.potencia, opt => opt.MapFrom(src => src.power))
                .ForMember(dest => dest.combustivel, opt => opt.MapFrom(src => src.fuel))
                .ForMember(dest => dest.chassi, opt => opt.MapFrom(src => src.chassis))
                .ForMember(dest => dest.uf, opt => opt.MapFrom(src => src.uf))
                .ForMember(dest => dest.municipio, opt => opt.MapFrom(src => src.Municipality))
                .ForMember(dest => dest.segmento, opt => opt.MapFrom(src => src.segment))
                .ForMember(dest => dest.especieVeiculo, opt => opt.MapFrom(src => src.VehicleType));

            CreateMap<Dictionary<string, string>, Automovel>()
                .ForMember(dest => dest.plate, opt => opt.MapFrom(src => GetValueOrDefault(src, "PLATE")))
                .ForMember(dest => dest.brand, opt => opt.MapFrom(src => GetValueOrDefault(src, "MARCA")))
                .ForMember(dest => dest.model, opt => opt.MapFrom(src => GetValueOrDefault(src, "MODELO")))
                .ForMember(dest => dest.imported, opt => opt.MapFrom(src => GetValueOrDefault(src, "IMPORTADO")))
                .ForMember(dest => dest.year, opt => opt.MapFrom(src => GetValueOrDefault(src, "ANO")))
                .ForMember(dest => dest.modelYear, opt => opt.MapFrom(src => GetValueOrDefault(src, "ANOMODELO")))
                .ForMember(dest => dest.color, opt => opt.MapFrom(src => GetValueOrDefault(src, "COR")))
                .ForMember(dest => dest.displacement, opt => opt.MapFrom(src => GetValueOrDefault(src, "CILINDRADA")))
                .ForMember(dest => dest.power, opt => opt.MapFrom(src => GetValueOrDefault(src, "POTENCIA")))
                .ForMember(dest => dest.fuel, opt => opt.MapFrom(src => GetValueOrDefault(src, "COMBUSTIVEL")))
                .ForMember(dest => dest.chassis, opt => opt.MapFrom(src => GetValueOrDefault(src, "CHASSI")))
                .ForMember(dest => dest.uf, opt => opt.MapFrom(src => GetValueOrDefault(src, "UF")))
                .ForMember(dest => dest.Municipality, opt => opt.MapFrom(src => GetValueOrDefault(src, "MUNICIPIO")))
                .ForMember(dest => dest.segment, opt => opt.MapFrom(src => GetValueOrDefault(src, "SEGMENTO")))
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => GetValueOrDefault(src, "ESPECIEVEICULO")));
        }
        string GetValueOrDefault(Dictionary<string, string> dictionary, string key)
        {
            return dictionary.TryGetValue(key, out string value) ? value : "";
        }
    }
}
