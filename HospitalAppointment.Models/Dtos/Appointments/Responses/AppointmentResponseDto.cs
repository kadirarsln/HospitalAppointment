namespace HospitalAppointment.Models.Dtos.Appointments.Responses;

public record AppointmentResponseDto
(
    Guid Id,
    string PatientName,
    DateTime AppointmentDate,
    int DoctorId,
    string DoctorName
);
