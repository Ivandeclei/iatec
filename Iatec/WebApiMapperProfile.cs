using AutoMapper;
using DomainLayer.Models;
using Iatec.Dtos;
namespace Iatec
{

    public class WebApiMapperProfile : Profile
    {
        public WebApiMapperProfile()
        {
            CreateMap<BaseEntity, BaseEntityDto>().ReverseMap();
            CreateMap<Participant, ParticipantDto>().ReverseMap();
            CreateMap<Participant, ParticipantDtoPut>()
                .ReverseMap();
            CreateMap<Participant, ParticipantResult>().ReverseMap();
            CreateMap<Event, EventResult>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Event, EventBaseDto>().ReverseMap();
            CreateMap<EventParticipant, EventParticipantDto>().ReverseMap();
            CreateMap<EventParticipant, ParticipantEventPost>().ReverseMap();
            CreateMap<EventFilterParameters, EventFilterParametersDto>().ReverseMap();
            CreateMap<PaginateFilter, PaginateFilterDto>().ReverseMap();
            CreateMap<User, UserBaseDto>().ReverseMap();

            CreateMap<EventParticipant, EventBaseDto>()
                .ForMember(u => u.Id, opt => opt.MapFrom(p => p.EventsId))
                .ForMember(u => u.Name, opt => opt.MapFrom(p => p.Event.Name))
                .ForMember(u => u.Description, opt => opt.MapFrom(p => p.Event.Description))
                .ForMember(u => u.EventPlace, opt => opt.MapFrom(p => p.Event.EventPlace))
                .ForMember(u => u.TypeEvent, opt => opt.MapFrom(p => p.Event.TypeEvent))
                .ForMember(u => u.Status, opt => opt.MapFrom(p => p.Event.Status))
                .ForMember(u => u.DateEvent, opt => opt.MapFrom(p => p.Event.DateEvent))
                .ForMember(u => u.ParticipantId, opt => opt.MapFrom(p => p.Event.ParticipantId))
                .ReverseMap();

            CreateMap<EventParticipant, ParticipantDto>()
                .ForMember(u => u.Name, opt => opt.MapFrom(p => p.Participant.Name))
                .ReverseMap();

            CreateMap<EventParticipant, ParticipantDtoPut>()
                .ForMember(u => u.Id, opt => opt.MapFrom(p => p.ParticipantsId))
                //.ForMember(u => u.Participant, opt => opt.MapFrom(p => p.Participant))
                .ReverseMap();


            CreateMap<Participant, UserDto>()
                .ForMember(u => u.Email, opt => opt.MapFrom(p => p.User.Email))
                .ForMember(u => u.Password, opt => opt.MapFrom(p => p.User.Password))
                .ForMember(u => u.Participant, opt => opt.MapFrom(p => p)).ReverseMap();

            CreateMap<Event, EventDto>()
                .ForMember(u => u.ParticipantId, opt => opt.MapFrom(p => p.ParticipantId))
                .ForMember(u => u.Name, opt => opt.MapFrom(p => p.Name))
                .ForMember(u => u.Description, opt => opt.MapFrom(p => p.Description))
                .ForMember(u => u.EventPlace, opt => opt.MapFrom(p => p.EventPlace))
                .ForMember(u => u.TypeEvent, opt => opt.MapFrom(p => p.TypeEvent))
                .ForMember(u => u.Status, opt => opt.MapFrom(p => p.Status))
                .ForMember(u => u.DateEvent, opt => opt.MapFrom(p => p.DateEvent))
                .ForMember(u => u.EventParticipant, opt => opt.MapFrom(p => p.Participants)).ReverseMap();
        }
    }
    
}