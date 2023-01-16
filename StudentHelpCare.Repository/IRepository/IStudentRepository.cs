using StudentHelpCare.Data.Entity;

namespace StudentHelpCare.Repository.IRepository
{
    public interface IStudentRepository
    {
        Task<bool> InsertItemAsync(StudentEntity entity);
        Task<List<StudentEntity>> GetItemListAsync();
    }
}
