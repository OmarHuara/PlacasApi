using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlacasAPI.Models
{
    public class Automovel
    {
        [Key]
        public int Id { get; set; }
        public string? plate { get; set; }
        public string? brand { get; set; }
        public string? model { get; set; }
        public string? modelYear { get; set; }
        public string? imported { get; set; }
        public string? year { get; set; }
        public string? color { get; set; }
        public string? displacement { get; set; }
        public string? power { get; set; }
        public string? fuel { get; set; }
        public string? chassis { get; set; }
        public string? uf { get; set; }
        public string? Municipality { get; set; }
        public string? segment { get; set; }
        public string? VehicleType { get; set; }
    }
}
