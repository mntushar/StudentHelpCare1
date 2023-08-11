namespace SHCApiGateway.Library
{
    public interface ICryptography
    {
        string GenerateToken(string userId, string purpose, string securityStamp, DateTime validityTime);
    }
}
