using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Models;
using Api.Domain.Security;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _repository { get; set; }
        private IUserRepository _repositoryUser;
        private readonly IMapper _mapper;
        public UserService(IRepository<UserEntity> repository,
        IUserRepository repositoryUser, IMapper mapper)
        {
            _repositoryUser = repositoryUser;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(Guid Id)
        {
            return await _repository.DeletetAsync(Id);
        }

        public async Task<UserDTOCommon> Get(Guid Id)
        {
            var resultEntity = await _repository.SelectAsync(Id);
            return _mapper.Map<UserDTOCommon>(resultEntity);
        }

        public async Task<IEnumerable<UserDTOCommon>> GetAll()
        {
            var resultEntity = await _repository.SelectAllAsync();
            return _mapper.Map<IEnumerable<UserDTOCommon>>(resultEntity);
        }

        public async Task<UserDTOCreateResult> Post(UserDTO user)
        {
            user.Password = ComputeHashing.ComputeSha256Hash(user.Password);
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var resultEntity = await _repository.InsertAsync(entity);

            return _mapper.Map<UserDTOCreateResult>(resultEntity);
        }

        public async Task<UserDTOUpdateResult> Put(UserDTOCommon user)
        {
            var model = _mapper.Map<UserModel>(user);
            var entity = _mapper.Map<UserEntity>(model);
            var resultEntity = await _repositoryUser.UpdateUser(entity);

            return _mapper.Map<UserDTOUpdateResult>(resultEntity);
        }
    }
}