using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
         Task<UserDTOCommon> Get(Guid Id);
         Task<IEnumerable<UserDTOCommon>> GetAll();
         Task<UserDTOCreateResult> Post(UserDTO user);
         Task<UserDTOUpdateResult> Put(UserDTOCommon user);
         Task<bool> Delete(Guid Id);
    }
}