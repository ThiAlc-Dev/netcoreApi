using System.Linq;
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

        [Fact(DisplayName = "CRUD completo de usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task EPossivelRealizarCrudUsuario()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                UserRepository _repository = new UserRepository(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Nome = Faker.Name.FullName(),
                    Password = Faker.RandomNumber.Next(10, 30).ToString()
                };

                //inserindo usuario
                var _retorno = await _repository.InsertAsync(_entity);
                Assert.True(_retorno.Id != System.Guid.Empty);

                //atualizando usuario
                _entity.Nome = Faker.Name.First();
                var _registroAtualizado = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.Nome, _registroAtualizado.Nome);

                //verifica se o usuario existe
                var _registroExiste = await _repository.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                //selecionando por Id
                var _registroSelecionado = await _repository.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);

                //selecionando todos
                var _todosRegistros = await _repository.SelectAllAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 1);

                //removendo usuario
                var _removeRegistro = await _repository.DeletetAsync(_registroSelecionado.Id);
                Assert.True(_removeRegistro);

                //login com usuario padrao
                var _usuarioPadrao = await _repository.FindByLogin("helpdeskprog@outlook.com");
                Assert.NotNull(_usuarioPadrao);
            }
        }
    }
}