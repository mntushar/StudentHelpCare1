using StudentHelpCare.Repository.IRepository;
using StudentHelpCare.Services.IServices;
using StudentHelpCare.ViewModel.Student;

namespace StudentHelpCare.Services.Services
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
