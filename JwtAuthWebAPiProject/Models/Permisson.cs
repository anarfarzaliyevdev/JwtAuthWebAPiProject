namespace JwtAuthWebAPiProject.Models
{
    public class Permisson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
