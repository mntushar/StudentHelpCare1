using Microsoft.EntityFrameworkCore;
using StudentHelpCare.Data.Entity;
using StudentHelpCare.Repository.IRepository;

namespace StudentHelpCare.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<UserEntity> GetItem(string userName)
        {
            var item = await _appDbContext.User.FirstOrDefaultAsync(x => x.UserName == userName);

            if(item == null)
                item = new UserEntity();

            return item;
        }
    }
}
