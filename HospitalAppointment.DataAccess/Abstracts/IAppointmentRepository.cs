
using Core.Repositories;
using HospitalAppointment.Models.Entities;

namespace HospitalAppointment.DataAccess.Abstracts;

public interface IAppointmentRepository : IRepository<Appointment, Guid>
{
    Task<int> CountByDoctorIdAsync(int doctorId);
    Task<List<Appointment>> GetAllWithAppointmentDetails();
    Task<Appointment?> GetAppointmentWitDoctorByIdAsync(Guid id);

}
