
using Core.Repositories;
using HospitalAppointment.DataAccess.Abstracts;
using HospitalAppointment.DataAccess.Contexts;
using HospitalAppointment.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointment.DataAccess.Concretes;

public class EfDoctorRepository : EfRepositoryBase<BaseDbContext, Doctor, int>, IDoctorRepository
{
    public EfDoctorRepository(BaseDbContext context) : base(context)
    {

    }

    public IQueryable<Doctor> GetDoctorWithAppointments()
    {
        return Context.Doctors.Include(d => d.Appointments).AsQueryable();
    }

    public Task<Doctor?> GetDoctorWithAppointmentsByIdAsync(int id)
    {
        return Context.Doctors.Include(d => d.Appointments).FirstOrDefaultAsync(d => d.Id == id);
    }
}
