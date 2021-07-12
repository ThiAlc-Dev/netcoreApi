using System;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        public SigningConfiguration _signingConfiguration { get; set; }
        public TokenConfiguration _tokenConfiguration { get; set; }
        public IConfiguration _configuration { get; set; }

        public LoginService(IUserRepository repository,
            SigningConfiguration signingConfiguration,
            TokenConfiguration tokenConfiguration,
            IConfiguration configuration)
        {
            _repository = repository;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }
        public async Task<object> FindByLogin(LoginDTO user)
        {
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                var baseUser = new UserEntity();
                baseUser = await _repository.FindByLogin(user.Email.ToLower());

                //verificar a senha digitada com a que está no banco
                if (ComputeHashing.VerifyHash(user.Password, baseUser.Password))
                {
                    return TokenSecurity.Create(baseUser, _tokenConfiguration, _signingConfiguration);
                }
                else
                {
                    return new
                    {
                        authenticated = false,
                        message = "Senha incorreta."
                    };
                }
            }
            else
            {
                return null;
            }
        }
    }
}