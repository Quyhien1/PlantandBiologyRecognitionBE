using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.Sample;
using PlantandBiologyRecognition.DAL.Payload.Respond.Sample;

namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class SampleMapperProfile : Profile
    {
        public SampleMapperProfile()
        {
            CreateMap<CreateSampleRequest, Sample>();
            CreateMap<UpdateSampleRequest, Sample>();
            CreateMap<Sample, CreateSampleRespond>();
            CreateMap<Sample, GetSampleRespond>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Sample, UpdateSampleRespond>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Sample, DeleteSampleRespond>();
        }
    }
} 