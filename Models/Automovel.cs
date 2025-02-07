using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlacasAPI.Models
{
    public class Automovel
    {
        [Key]
        public int Id { get; set; }

        [JsonPropertyName("marca")]
        public string? brand { get; set; }

        [JsonPropertyName("modelo")]
        public string? model { get; set; }

        [JsonPropertyName("importado")]
        public string? imported { get; set; }

        [JsonPropertyName("ano")]
        public string? year { get; set; }

        [JsonPropertyName("cor")]
        public string? color { get; set; }

        [JsonPropertyName("cilindrada")]
        public string? displacement { get; set; }

        [JsonPropertyName("potencia")]
        public string? power { get; set; }

        [JsonPropertyName("combustivel")]
        public string? fuel { get; set; }

        [JsonPropertyName("uf")]
        public string? chassis { get; set; }

        [JsonPropertyName("ispb")]
        public string? uf { get; set; }

        [JsonPropertyName("municipio")]
        public string? Municipality { get; set; }

        [JsonPropertyName("segmento")]
        public string? segment { get; set; }

        [JsonPropertyName("especieVeiculo")]
        public string? VehicleType { get; set; }
    }
}
