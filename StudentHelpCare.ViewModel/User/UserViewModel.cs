using System.Globalization;

namespace StudentHelpCare.ViewModel.User
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? RepeatPassword { get; set; }
    }
}
