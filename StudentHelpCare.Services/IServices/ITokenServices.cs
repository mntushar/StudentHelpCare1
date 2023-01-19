using StudentHelpCare.Data.Entity;

namespace StudentHelpCare.Services.IServices
{
    public interface ITokenServices
    {
        Task<string> GenerateTocken(UserEntity user);
    }
}
