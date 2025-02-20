﻿namespace HospitalAppointment.Models.Dtos.Appointments.Requests;

public record UpdateAppointmentRequest
(
    Guid Id,
    string PatientName,
    DateTime AppointmentDate,
    int DoctorId
);
