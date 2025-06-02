using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.Account;
using PlantandBiologyRecognition.DAL.Payload.Respond.Account;
namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<CreateAccountRequest, Account>()
                .ForMember(dest => dest.Accountid, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));
            CreateMap<Account, CreateAccountRespond>()
                .ForMember(dest => dest.Accountid, opt => opt.MapFrom(src => src.Accountid))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Roleid, opt => opt.MapFrom(src => src.Roleid));

        }
    }
}
