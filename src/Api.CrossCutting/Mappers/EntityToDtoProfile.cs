using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappers
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<UserDTOCommon, UserEntity>().ReverseMap();
            CreateMap<UserDTOCreateResult, UserEntity>().ReverseMap();
            CreateMap<UserDTOUpdateResult, UserEntity>().ReverseMap();
        }
    }
}