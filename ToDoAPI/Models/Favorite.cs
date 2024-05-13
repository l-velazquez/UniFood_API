using System.ComponentModel.DataAnnotations;

namespace UniFood.Models
{
    public class Favorite
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RestaurantId { get; set; }
    }
}