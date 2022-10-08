namespace JwtAuthWebAPiProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string  RefreshToken { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Permisson> Permissions { get; set; }
    }
}
