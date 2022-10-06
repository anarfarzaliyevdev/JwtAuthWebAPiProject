using System.ComponentModel.DataAnnotations;

namespace JwtAuthWebAPiProject.DTOs
{
    public class LoginInputModel
    {
        [Required(ErrorMessage ="Email field is required")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
