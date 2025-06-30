using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.LearningTip;
using PlantandBiologyRecognition.DAL.Payload.Respond.LearningTip;

namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class LearningTipMapper : Profile
    {
        public LearningTipMapper()
        {
            CreateMap<CreateLearningTipRequest, Learningtip>()
                .ForMember(dest => dest.TipId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
                .ForMember(dest => dest.TipText, opt => opt.MapFrom(src => src.TipText));

            CreateMap<Learningtip, CreateLearningTipRespond>()
                .ForMember(dest => dest.TipId, opt => opt.MapFrom(src => src.TipId))
                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
                .ForMember(dest => dest.TipText, opt => opt.MapFrom(src => src.TipText));

            CreateMap<UpdateLearningTipRequest, Learningtip>()
                .ForMember(dest => dest.TipId, opt => opt.MapFrom(src => src.TipId))
                .ForMember(dest => dest.TipText, opt => opt.MapFrom(src => src.TipText));

            CreateMap<Learningtip, UpdateLearningTipRespond>()
                .ForMember(dest => dest.TipId, opt => opt.MapFrom(src => src.TipId))
                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
                .ForMember(dest => dest.UpdatedTipText, opt => opt.MapFrom(src => src.TipText));

            CreateMap<Learningtip, GetLearningTipRespond>()
                .ForMember(dest => dest.TipId, opt => opt.MapFrom(src => src.TipId))
                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
                .ForMember(dest => dest.TipText, opt => opt.MapFrom(src => src.TipText));
        }
    }
}