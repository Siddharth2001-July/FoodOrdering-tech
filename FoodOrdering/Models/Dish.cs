using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Models
{
    public class Dish
    {
        [Key]
        public string DishId { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Enter a Dish Name")]
        public string DishName { get; set; }

        [Required(ErrorMessage = "Enter valid Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Quantity { get; set; } = 1;
    }
}
