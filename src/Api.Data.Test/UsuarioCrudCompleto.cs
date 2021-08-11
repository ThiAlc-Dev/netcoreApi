using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using static Api.Data.Test.BaseTest;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.serviceProvider;
        }

        [Fact(DisplayName = "CRUD cadastro de usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task EPossivelRealizarCrudUsuario()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                UserRepository _repository = new UserRepository(context);
                UserEntity _entity = new UserEntity
                {
                    Email = "teste@teste.com",
                    Nome = "teste criação",
                    Password = "abc@123"
                };

                var _retorno = await _repository.InsertAsync(_entity);
                Assert.True(_retorno.Id != System.Guid.Empty);
            }
        }
    }
}