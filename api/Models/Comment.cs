namespace FinShark.api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }   //the actual key. entity framework will set up the relationship
        public Stock? Stock { get; set; }   //navigation property : will allow us to navigate within the models
        
    }
}
