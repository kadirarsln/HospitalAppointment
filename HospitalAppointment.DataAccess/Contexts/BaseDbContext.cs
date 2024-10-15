using HospitalAppointment.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointment.DataAccess.Contexts;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
}
