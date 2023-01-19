using StudentHelpCare.ViewModel.User;

namespace StudentHelpCare.Services.IServices
{
    public interface IUserServices
    {
        Task<UserViewModel> GetItem(string userName);
    }
}
