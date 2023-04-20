namespace SHCApiGateway.Data.Model
{
    public class AuthenticationResult
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string? Message { get; set; }
    }
}
