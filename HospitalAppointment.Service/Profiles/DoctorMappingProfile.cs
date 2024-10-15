using AutoMapper;
using HospitalAppointment.Models.Dtos.Doctors.Requests;
using HospitalAppointment.Models.Dtos.Doctors.Responses;
using HospitalAppointment.Models.Entities;

namespace HospitalAppointment.Service.Profiles;

public class DoctorMappingProfile : Profile
{

    public DoctorMappingProfile()
    {
        CreateMap<Doctor, DoctorResponseDto>()
        .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch.ToString())).ReverseMap();

        CreateMap<Doctor, DoctorWithAppointmensDto>()
            .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments));

        CreateMap<CreateDoctorRequest, Doctor>();

        CreateMap<UpdateDoctorRequest, Doctor>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch));
    }
}
