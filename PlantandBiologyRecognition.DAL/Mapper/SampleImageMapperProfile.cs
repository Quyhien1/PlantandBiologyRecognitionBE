using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.SampleImage;
using PlantandBiologyRecognition.DAL.Payload.Respond.SampleImage;

namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class SampleImageMapperProfile : Profile
    {
        public SampleImageMapperProfile()
        {
            CreateMap<CreateSampleImageRequest, Sampleimage>();
            CreateMap<UpdateSampleImageRequest, Sampleimage>();
            CreateMap<Sampleimage, CreateSampleImageRespond>();
            CreateMap<Sampleimage, GetSampleImageRespond>()
                .ForMember(dest => dest.SampleName, opt => opt.MapFrom(src => src.Sample.Name));
            CreateMap<Sampleimage, UpdateSampleImageRespond>()
                .ForMember(dest => dest.SampleName, opt => opt.MapFrom(src => src.Sample.Name));
            CreateMap<Sampleimage, DeleteSampleImageRespond>();
        }
    }
}