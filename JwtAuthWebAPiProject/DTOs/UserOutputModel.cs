using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.DTOs
{
    public class UserOutputModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
       
    }
}
