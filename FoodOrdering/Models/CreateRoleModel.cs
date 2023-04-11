using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Models
{
    public class CreateRoleModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}