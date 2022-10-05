namespace JwtAuthWebAPiProject.DTOs
{
    public class UpdateEmployeeInputModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
    }
}
