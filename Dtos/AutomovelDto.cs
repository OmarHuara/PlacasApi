using System.Text.Json;
using ThirdParty.Json.LitJson;

namespace PlacasAPI.Dtos
{
    public class AutomovelDto
    {
            public string? placa { get; set; }
            public string? marca { get; set; }
            public string? modelo { get; set; }
            public string? importado { get; set; }
            public string? ano { get; set; }
            public string? AnoModelo { get; set; }
            public string? cor { get; set; }
            public string? cilindrada { get; set; }
            public string? potencia { get; set; }
            public string? combustivel { get; set; }
            public string? chassi { get; set; }
            public string? uf { get; set; }
            public string? municipio { get; set; }
            public string? segmento { get; set; }
            public string? especieVeiculo { get; set; }
            public string? carroceria { get; set; }
            public string? capacidade { get; set; }

    }
}
