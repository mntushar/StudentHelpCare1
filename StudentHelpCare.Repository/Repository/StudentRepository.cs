using Microsoft.EntityFrameworkCore;
using StudentHelpCare.Data.Entity;
using StudentHelpCare.Repository.IRepository;

namespace StudentHelpCare.Repository.Repository
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
