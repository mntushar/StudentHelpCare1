using StudentHelpCare.StudentHelpCare.Data.Entity;

namespace StudentHelpCare.StudentHelpCare.Repository.IRepository
{
    public interface IStudentRepository
    {
        Task<bool> InsertItemAsync(StudentEntity entity);
        Task<List<StudentEntity>> GetItemListAsync();
    }
}
