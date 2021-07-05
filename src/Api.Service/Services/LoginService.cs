using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        public LoginService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<object> FindByLogin(UserEntity user)
        {
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                return await _repository.FindByLogin(user.Email.ToLower());
            }
            else
            {
                return null;
            }
        }
    }
}