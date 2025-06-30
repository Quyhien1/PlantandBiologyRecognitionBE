//using AutoMapper;
//using PlantandBiologyRecognition.DAL.Models;
//using PlantandBiologyRecognition.DAL.Payload.Request.TextbookLink;
//using PlantandBiologyRecognition.DAL.Payload.Respond.TextbookLink;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlantandBiologyRecognition.DAL.Mapper
//{
//    public class TextbooklinkMapper : Profile
//    {
//        public TextbooklinkMapper()
//        {
//            CreateMap<CreateTextbooklinkRequest, Textbooklink>()
//               .ForMember(dest => dest.LinkId, opt => opt.MapFrom(_ => Guid.NewGuid()))
//               .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
//               .ForMember(dest => dest.TextbookName, opt => opt.MapFrom(src => src.TextbookName))
//               .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
//               .ForMember(dest => dest.ContentSummary, opt => opt.MapFrom(src => src.ContentSummary));

//            CreateMap<UpdateTextbooklinkRequest, Textbooklink>()
//                .ForMember(dest => dest.LinkId, opt => opt.MapFrom(src => src.LinkId))
//                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
//                .ForMember(dest => dest.TextbookName, opt => opt.MapFrom(src => src.TextbookName))
//                .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
//                .ForMember(dest => dest.ContentSummary, opt => opt.MapFrom(src => src.ContentSummary));

//            CreateMap<Textbooklink, CreateTextbooklinkRespond>()
//                .ForMember(dest => dest.LinkId, opt => opt.MapFrom(src => src.LinkId))
//                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
//                .ForMember(dest => dest.TextbookName, opt => opt.MapFrom(src => src.TextbookName))
//                .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
//                .ForMember(dest => dest.ContentSummary, opt => opt.MapFrom(src => src.ContentSummary));

//            CreateMap<Textbooklink, UpdateTextbooklinkRespond>()
//                .ForMember(dest => dest.LinkId, opt => opt.MapFrom(src => src.LinkId))
//                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
//                .ForMember(dest => dest.TextbookName, opt => opt.MapFrom(src => src.TextbookName))
//                .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
//                .ForMember(dest => dest.ContentSummary, opt => opt.MapFrom(src => src.ContentSummary));

//            CreateMap<Textbooklink, GetTextbooklinkRespond>()
//                .ForMember(dest => dest.LinkId, opt => opt.MapFrom(src => src.LinkId))
//                .ForMember(dest => dest.SampleId, opt => opt.MapFrom(src => src.SampleId))
//                .ForMember(dest => dest.TextbookName, opt => opt.MapFrom(src => src.TextbookName))
//                .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
//                .ForMember(dest => dest.ContentSummary, opt => opt.MapFrom(src => src.ContentSummary));
//        }
//    }
//}
