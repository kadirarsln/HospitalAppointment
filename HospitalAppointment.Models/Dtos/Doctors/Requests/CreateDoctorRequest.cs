using HospitalAppointment.Models.Enums;

namespace HospitalAppointment.Models.Dtos.Doctors.Requests;

public record CreateDoctorRequest
    (
    string Name,
    Branch Branch
    );
