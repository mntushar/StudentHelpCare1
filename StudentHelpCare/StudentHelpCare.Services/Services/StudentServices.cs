using StudentHelpCare.StudentHelpCare.Repository.IRepository;
using StudentHelpCare.StudentHelpCare.Services.IServices;
using StudentHelpCare.StudentHelpCare.ViewModel.Student;

namespace StudentHelpCare.StudentHelpCare.Services.Services
{
    public class StudentServices : IStudentServices
    {
        private IStudentRepository _studentRepository;

        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> InsertItemAsync(StudentViewModal entity)
        {
            return await _studentRepository.InsertItemAsync(StudentDto.Map(entity));
        }

        public async Task<IEnumerable<StudentViewModal>> GetItemListAsync()
        {
            return StudentDto.Map(await _studentRepository.GetItemListAsync());
        }
    }
}
