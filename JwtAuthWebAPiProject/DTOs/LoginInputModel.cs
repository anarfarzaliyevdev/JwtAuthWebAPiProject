using System.ComponentModel.DataAnnotations;

namespace JwtAuthWebAPiProject.DTOs
{
    public class LoginInputModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
