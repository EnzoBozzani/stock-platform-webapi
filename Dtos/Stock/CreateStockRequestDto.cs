using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol can't be over 10 characters!")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Company name can't be over 10 characters!")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1E12)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Industry can't be over 20 characters!")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5E12)]
        public long MarketCap { get; set; }
    }
}