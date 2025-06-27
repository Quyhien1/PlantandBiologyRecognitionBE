using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.SavedSample;
using PlantandBiologyRecognition.DAL.Payload.Respond.SavedSample;

namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class SavedSampleMapperProfile : Profile
    {
        public SavedSampleMapperProfile()
        {
            CreateMap<CreateSavedSampleRequest, Savedsample>();
            CreateMap<UpdateSavedSampleRequest, Savedsample>();
            CreateMap<Savedsample, CreateSavedSampleRespond>();
            CreateMap<Savedsample, GetSavedSampleRespond>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.SampleName, opt => opt.MapFrom(src => src.Sample.Name));
            CreateMap<Savedsample, UpdateSavedSampleRespond>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.SampleName, opt => opt.MapFrom(src => src.Sample.Name));
            CreateMap<Savedsample, DeleteSavedSampleRespond>()
                .ForMember(dest => dest.SampleName, opt => opt.MapFrom(src => src.Sample.Name));
        }
    }
}