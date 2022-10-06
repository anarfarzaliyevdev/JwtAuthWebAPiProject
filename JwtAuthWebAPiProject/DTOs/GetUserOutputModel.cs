using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.DTOs
{
    public class GetUserOutputModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<Permisson> Permissions { get; set; }
    }
}
