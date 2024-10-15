
using Core.Entities;
using HospitalAppointment.Models.Enums;

namespace HospitalAppointment.Models.Entities;

public sealed class Doctor:Entity<int>
{
    public Doctor()
    {
        Name = string.Empty;
        Appointments = new List<Appointment>();
    }

    public string Name { get; set; }
    public Branch Branch { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; }
}
