using System.ComponentModel.DataAnnotations;

namespace JwtAuthWebAPiProject.DTOs
{
    public class CreateUserInputModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
