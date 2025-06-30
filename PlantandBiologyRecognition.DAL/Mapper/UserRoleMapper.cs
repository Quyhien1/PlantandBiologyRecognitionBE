using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.UserRole;
using PlantandBiologyRecognition.DAL.Payload.Respond.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class UserRoleMapper : Profile
    {
        public UserRoleMapper()
        {
            CreateMap<CreateUserRoleRequest, Userrole>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName.ToString()));

            CreateMap<Userrole, UserRoleRespond>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<UpdateUserRoleRequest, Userrole>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName.ToString()))
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
