using Core.Repositories;
using HospitalAppointment.Models.Entities;

namespace HospitalAppointment.DataAccess.Abstracts;

public interface IDoctorRepository : IRepository<Doctor, int>
{
    Task<Doctor?> GetDoctorWithAppointmentsByIdAsync(int id);
    IQueryable<Doctor> GetDoctorWithAppointments();
}
