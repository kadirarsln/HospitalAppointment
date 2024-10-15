namespace HospitalAppointment.Models.Dtos.Appointments.Requests;

public record CreateAppointmentRequest
(
    string PatientName,
    DateTime AppointmentDate,
    int DoctorId
);