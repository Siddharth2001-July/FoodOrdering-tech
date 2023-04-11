using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is Required")] 
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;
    }
}