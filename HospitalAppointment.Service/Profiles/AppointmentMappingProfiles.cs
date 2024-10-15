using AutoMapper;
using HospitalAppointment.Models.Dtos.Appointments.Requests;
using HospitalAppointment.Models.Dtos.Appointments.Responses;
using HospitalAppointment.Models.Entities;

namespace HospitalAppointment.Service.Profiles;
public class AppointmentMappingProfiles : Profile
{
    public AppointmentMappingProfiles()
    {
        CreateMap<Appointment, AppointmentResponseDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.Name));

        {
            CreateMap<CreateAppointmentRequest, Appointment>().ReverseMap();

            CreateMap<UpdateAppointmentRequest, Appointment>().ReverseMap();

        }


    }
}
