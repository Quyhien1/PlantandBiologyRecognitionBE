using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request;
using PlantandBiologyRecognition.DAL.Payload.Respond;

namespace PlantandBiologyRecognition.DAL.Mapper
{
    public class FeedbackMapper : Profile
    {
        public FeedbackMapper() { 
            CreateMap<CreateFeedbackRequest, Feedback>()
                .ForMember(dest => dest.FeedbackId, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.SubmittedAt, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<Feedback, CreateFeedbackRespond>()
                .ForMember(dest => dest.FeedbackId, opt => opt.MapFrom(src => src.FeedbackId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.SubmittedAt, opt => opt.MapFrom(src => src.SubmittedAt.HasValue
                ? DateOnly.FromDateTime(src.SubmittedAt.Value): default));


        }
    }
}
