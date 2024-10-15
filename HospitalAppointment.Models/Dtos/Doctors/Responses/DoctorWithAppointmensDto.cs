using HospitalAppointment.Models.Dtos.Appointments.Responses;

namespace HospitalAppointment.Models.Dtos.Doctors.Responses;

public record DoctorWithAppointmensDto
(
    int Id,
string Name,
string Branch,
DateTime CreatedDate,
List<AppointmentResponseDto> Appointments
);

