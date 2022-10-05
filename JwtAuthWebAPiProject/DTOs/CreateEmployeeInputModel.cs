namespace JwtAuthWebAPiProject.DTOs
{
    public class CreateEmployeeInputModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
    }
}
