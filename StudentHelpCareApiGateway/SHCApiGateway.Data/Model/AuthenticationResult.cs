namespace SHCApiGateway.Data.Model
{
    public class AuthenticationResult
    {
        public bool Success { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string? Message { get; set; }
    }
}
