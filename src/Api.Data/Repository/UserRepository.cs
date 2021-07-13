using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private static DbSet<UserEntity> _dataset;

        public UserRepository(MyContext context) : base(context)
        {
            _dataset = context.Set<UserEntity>();
        }
        public async Task<UserEntity> FindByLogin(string email)
        {
            return await _dataset.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(user.Id));
            if (result == null)
                return null;

            user.UpdateAt = DateTime.UtcNow;
            user.CreateAt = result.CreateAt;
            user.Password = result.Password;

            _context.Entry(result).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}