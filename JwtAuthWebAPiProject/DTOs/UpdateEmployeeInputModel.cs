using System.ComponentModel.DataAnnotations;

namespace JwtAuthWebAPiProject.DTOs
{
    public class UpdateEmployeeInputModel
    {
        [Required(ErrorMessage ="Id field must be entered")]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
    }
}
