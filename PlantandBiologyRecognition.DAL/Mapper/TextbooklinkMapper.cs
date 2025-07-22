using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.TextbookLink;
using PlantandBiologyRecognition.DAL.Payload.Respond.TextbookLink;

namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class TextbooklinkMapper : Profile
    {
        public TextbooklinkMapper()
        {
            CreateMap<CreateTextbooklinkRequest, Textbooklink>()
                .ForMember(dest => dest.LinkId, opt => opt.Ignore())
                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
                .ForMember(dest => dest.TextbookName, opt => opt.MapFrom(src => src.TextbookName))
                .ForMember(dest => dest.Chapter, opt => opt.MapFrom(src => src.Chapter))
                .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson));

            CreateMap<UpdateTextbooklinkRequest, Textbooklink>()
                .ForMember(dest => dest.LinkId, opt => opt.MapFrom(src => src.LinkId))
                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
                .ForMember(dest => dest.TextbookName, opt => opt.MapFrom(src => src.TextbookName))
                .ForMember(dest => dest.Chapter, opt => opt.MapFrom(src => src.Chapter))
                .ForMember(dest => dest.Lesson, opt => opt.MapFrom(src => src.Lesson));

            CreateMap<Textbooklink, CreateTextbooklinkRespond>();
            CreateMap<Textbooklink, UpdateTextbooklinkRespond>();
            CreateMap<Textbooklink, GetTextbooklinkRespond>();
        }
    }
}
