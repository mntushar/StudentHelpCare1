namespace StudentHelpCare.Data.Model
{
    public class UserLoginModel
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsRemember { get; set; }
    }
}
