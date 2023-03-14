using StudentHelpCare.StudentHelpCare.ViewModel.Student;

namespace StudentHelpCare.StudentHelpCare.Services.IServices
{
    public interface IStudentServices
    {
        Task<bool> InsertItemAsync(StudentViewModal entity);
        Task<IEnumerable<StudentViewModal>> GetItemListAsync();
    }
}
