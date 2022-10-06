namespace JwtAuthWebAPiProject.DTOs
{
    public class TokenOutputModel
    {
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public int UserId { get; set; }
    }
}
