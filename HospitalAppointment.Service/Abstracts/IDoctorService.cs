using Core.Models;
using HospitalAppointment.Models.Dtos.Doctors.Requests;
using HospitalAppointment.Models.Dtos.Doctors.Responses;
using HospitalAppointment.Models.Entities;

namespace HospitalAppointment.Service.Abstracts;

public interface IDoctorService
{
    Task<ReturnModel<List<DoctorResponseDto>>> GetAllAsync();
    Task<ReturnModel<DoctorResponseDto?>> GetByIdAsync(int id);
    Task<ReturnModel<Doctor?>> AddAsync(CreateDoctorRequest request);
    Task<ReturnModel<Doctor?>> UpdateAsync(UpdateDoctorRequest request);
    Task<ReturnModel<Doctor>> RemoveAsync(int id);
    Task<ReturnModel<List<DoctorWithAppointmensDto>>> GetDoctorWithAppointmentsAsync();
    Task<DoctorWithAppointmensDto> GetDoctorWithAppointmentsByIdAsync(int doctorId);

}
