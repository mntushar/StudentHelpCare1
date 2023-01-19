using StudentHelpCare.Data.Entity;

namespace StudentHelpCare.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<UserEntity> GetItem(string userName);
    }
}
