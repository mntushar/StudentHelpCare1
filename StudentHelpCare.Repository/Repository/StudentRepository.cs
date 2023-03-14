using Microsoft.EntityFrameworkCore;
using StudentHelpCare.StudentHelpCare.Data.Entity;
using StudentHelpCare.StudentHelpCare.Repository.IRepository;

namespace StudentHelpCare.StudentHelpCare.Repository.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private AppDbContext _appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public virtual async Task<bool> InsertItemAsync(StudentEntity entity)
        {
            await _appDbContext.Student.AddAsync(entity);
            return await _appDbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public virtual async Task<List<StudentEntity>> GetItemListAsync()
        {
            return await _appDbContext.Student.ToListAsync();
        }
    }
}
