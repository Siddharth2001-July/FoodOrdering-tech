using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Models
{
    public class OrderModel
    {
        [Key]
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string CustomerEmail { get; set; }
        [Required(ErrorMessage = "Enter valid order dishes")]
        public List<Dish> Dishes { get; set; } = new List<Dish>();
        public DateTime OrderTime { get; set; } = DateTime.Now;
    }
}
