using Api.Domain.DTOs.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappers
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDTO>().ReverseMap();
            CreateMap<UserModel, UserDTOCommon>().ReverseMap();
        }
    }
}