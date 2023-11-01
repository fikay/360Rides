using _360.Models;
using _360.Models.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.Utility
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {

            CreateMap<ServiceRequest, CartDTO>().ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.ServiceName));
            CreateMap<ServicesModel, ServicesDTO>();
            CreateMap<ServicesModel, ServicesAdminDTO>().ReverseMap();
            CreateMap<ServiceRequest, ServiceRequestDTO>().ForMember(dest => dest.childrenName, opt => opt.MapFrom(src => src.childrenNames.Select(cn => cn.Name).ToList()));
            //CreateMap<ServiceRequest, ServiceRequestDTO>().ForMember(dest => dest.childId, opt => opt.MapFrom(src => src.childrenNames.Select(cn => cn.Id).ToList()));

            // ServiceRequestDTO to ServiceRequest
            CreateMap<ServiceRequestDTO, ServiceRequest>()
                .ForMember(dest => dest.childrenNames, opt => opt.MapFrom(src => src.childrenName.Select(cn => new ChildrenName { Name = cn }).ToList()));
               
        }
    }
}
