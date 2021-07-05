using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dbset;

        public UserRepository(MyContext context) : base(context)
        {
            _dbSet = context.Set<UserEntity>();
        }
        public async Task<UserEntity> FindByLogin(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        }
    }
}