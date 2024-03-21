using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public string AppUserId { get; set; }
        public Guid StockId { get; set; }
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }
    }
}