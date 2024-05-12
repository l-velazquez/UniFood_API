namespace UniFood.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int PlaceId { get; set; } // Foreign key relationship to Place
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? Modified { get; set; } 
    }
}
