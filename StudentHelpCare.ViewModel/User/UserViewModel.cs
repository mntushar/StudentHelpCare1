using System.Globalization;

namespace StudentHelpCare.StudentHelpCare.ViewModel.User
{
    public class UserViewModel
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!; 
        public string Password { get; set; } = null!;
    }
}
