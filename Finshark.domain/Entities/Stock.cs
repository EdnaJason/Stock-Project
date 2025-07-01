using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.Domain.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]    //to ensure it is a monetary amt, this limits it to 18 digits and 2 decimal pts
        public int Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public int LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
         
        public List<Comment> Comments { get; set; } = new List<Comment>(); 

    }
}
