using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.SampleDetail;
using PlantandBiologyRecognition.DAL.Payload.Respond.SampleDetail;

namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class SampleDetailMapperProfile : Profile
    {
        public SampleDetailMapperProfile()
        {
            CreateMap<CreateSampleDetailRequest, Sampledetail>();
            CreateMap<UpdateSampleDetailRequest, Sampledetail>();
            CreateMap<Sampledetail, CreateSampleDetailRespond>();
            CreateMap<Sampledetail, GetSampleDetailRespond>()
                .ForMember(dest => dest.SampleName, opt => opt.MapFrom(src => src.Sample.Name));
            CreateMap<Sampledetail, UpdateSampleDetailRespond>()
                .ForMember(dest => dest.SampleName, opt => opt.MapFrom(src => src.Sample.Name));
            CreateMap<Sampledetail, DeleteSampleDetailRespond>();
        }
    }
}