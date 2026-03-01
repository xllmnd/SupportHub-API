using AutoMapper;
using SupportHub.Application.Enums;
using SupportHub.Application.Models;
using SupportHub.Domain.Entities;
using SupportHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TicketStatus, TicketStatusDto>();
            CreateMap<TicketPriority, TicketPriorityDto>();
            CreateMap<TicketCategory, TicketCategoryDto>();

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Customer, CustomerWithTicketsDto>()
                .IncludeBase<Customer, CustomerDto>(); // Inherit common fields from CustomerDto

            CreateMap<Agent, AgentDto>().ReverseMap();

            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))

                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.AssignedAgentName, opt => opt.MapFrom(src => src.AssignedAgent != null
                    ? src.AssignedAgent.DisplayName
                    : null));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent.DisplayName))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));

            
        }
    }
}
