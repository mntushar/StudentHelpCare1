namespace SHCApiGateway.ViewModel.UserClim
{
    public class UserClimViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}
