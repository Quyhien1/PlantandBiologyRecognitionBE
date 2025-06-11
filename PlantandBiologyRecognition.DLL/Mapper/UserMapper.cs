using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.User;
using PlantandBiologyRecognition.DAL.Payload.Respond.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<User, CreateUserRespond>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
