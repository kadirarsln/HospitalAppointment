using HospitalAppointment.Models.Enums;

namespace HospitalAppointment.Models.Dtos.Doctors.Requests;

public record UpdateDoctorRequest
    (
    int Id,
    string Name,
    Branch Branch
    );

