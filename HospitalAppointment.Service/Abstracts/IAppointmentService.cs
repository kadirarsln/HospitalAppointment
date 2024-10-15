using HospitalAppointment.Models.Dtos.Appointments.Responses;
using HospitalAppointment.Models.Dtos.Appointments.Requests;
using HospitalAppointment.Models.Entities;
using Core.Models;


namespace HospitalAppointment.Service.Abstracts;

public interface IAppointmentService
{
    Task<ReturnModel<List<AppointmentResponseDto>>> GetAllAsync();
    Task<ReturnModel<AppointmentResponseDto?>> GetByIdAsync(Guid id);
    Task<ReturnModel<Appointment?>> AddAsync(CreateAppointmentRequest request);
    Task<ReturnModel<Appointment?>> UpdateAsync(UpdateAppointmentRequest request);
    Task<ReturnModel<Appointment>> RemoveAsync(Guid id);


}
