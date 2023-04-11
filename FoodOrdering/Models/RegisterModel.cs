using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Enter your email address")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please choose a Password")]
        public string Password { get; set; } = string.Empty;
    }
}