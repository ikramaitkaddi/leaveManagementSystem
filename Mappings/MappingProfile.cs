using static System.Net.Mime.MediaTypeNames;


    using AutoMapper;
    using LeaveManagementSystem.DTOs;
    using LeaveManagementSystem.Models;
using LeaveManagementSystem.Enums;

namespace Infrastructure.Mappings
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Employee, EmployeeDto>();
                CreateMap<LeaveRequest, LeaveRequestDto>()
                    .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType.ToString()))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
                CreateMap<CreateLeaveRequestDto, LeaveRequest>()
                    .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => Enum.Parse<LeaveType>(src.LeaveType)))
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            }
        }
    }
