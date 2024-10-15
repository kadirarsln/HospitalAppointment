using HospitalAppointment.Models.Enums;

namespace HospitalAppointment.Models.Dtos.Doctors.Responses;

public record DoctorResponseDto
    (

    int Id,
    string Name,
    string Branch,
    DateTime CreatedDate

    );