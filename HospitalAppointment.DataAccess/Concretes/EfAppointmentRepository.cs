using Core.Repositories;
using HospitalAppointment.DataAccess.Abstracts;
using HospitalAppointment.DataAccess.Contexts;
using HospitalAppointment.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointment.DataAccess.Concretes;

public class EfAppointmentRepository : EfRepositoryBase<BaseDbContext, Appointment, Guid>, IAppointmentRepository

{
    public EfAppointmentRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<int> CountByDoctorIdAsync(int doctorId)
    {
        return await Context.Appointments
            .CountAsync(a => a.DoctorId == doctorId);
    }

    public async Task<List<Appointment>> GetAllWithAppointmentDetails()
    {
        return Context.Appointments.Include(d => d.Doctor).ToList();
    }

    public Task<Appointment?> GetAppointmentWitDoctorByIdAsync(Guid id)
    {
        return Context.Appointments.Include(d => d.Doctor).FirstOrDefaultAsync(d => d.Id ==id);
    }
}
