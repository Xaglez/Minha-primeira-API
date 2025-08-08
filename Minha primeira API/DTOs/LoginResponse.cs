namespace Minha_primeira_API.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public int Id { get; set; }
    }
}
