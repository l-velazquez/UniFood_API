namespace UniFood.Models
{
    public class Place
    {
        public int Id { get; set; }
        public int UniversityId { get; set; } // Foreign key relationship
        public string Name { get; set; }
        public string Address { get; set; }
        public string Schedule { get; set; } 
        public string PriceAverage { get; set; } 
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? Modified { get; set; } // Nullable for optional modification tracking
    }
}
