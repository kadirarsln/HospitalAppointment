using AutoMapper;
using Core.Exceptions;
using Core.Models;
using HospitalAppointment.DataAccess.Abstracts;
using HospitalAppointment.Models.Dtos.Appointments.Requests;
using HospitalAppointment.Models.Dtos.Appointments.Responses;
using HospitalAppointment.Models.Entities;
using HospitalAppointment.Service.Abstracts;
using System.Net;

namespace HospitalAppointment.Service.Concrete;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public AppointmentService(IMapper mapper, IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository)
    {
        _mapper = mapper;
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<ReturnModel<List<AppointmentResponseDto>>> GetAllAsync()
    {
        var appointments = await _appointmentRepository.GetAllWithAppointmentDetails();
        var appointmentDtos = _mapper.Map<List<AppointmentResponseDto>>(appointments);

        return new ReturnModel<List<AppointmentResponseDto>>
        {
            Success = true,
            Data = appointmentDtos,
            Message = "Appointments Listed.",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ReturnModel<AppointmentResponseDto?>> GetByIdAsync(Guid id)
    {
        var appointment = await _appointmentRepository.GetAppointmentWitDoctorByIdAsync(id);
        if (appointment == null)
        {
            throw new NotFoundException("Appointment not found.");
        }


        var appointmentDto = _mapper.Map<AppointmentResponseDto>(appointment);

        return new ReturnModel<AppointmentResponseDto?>
        {
            Success = true,
            Data = appointmentDto,
            Message = "Appointment found.",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ReturnModel<Appointment?>> AddAsync(CreateAppointmentRequest request)
    {

        if (request.AppointmentDate < DateTime.Now.AddDays(3))
        {
            throw new ValidationException("Appointment date must be at least 3 days from now.");
        }

        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
        {
            throw new ValidationException("Doctor not found.");
        }

        var totalAppointments = await _appointmentRepository.CountByDoctorIdAsync(request.DoctorId);
        if (totalAppointments >= 10)
        {
            throw new ValidationException("This doctor has already reached the maximum number of appointments.");
        }

        var appointment = _mapper.Map<Appointment>(request);
        await _appointmentRepository.AddAsync(appointment);

        return new ReturnModel<Appointment?>
        {
            Success = true,
            Data = appointment,
            Message = "Appointment is added.",
            StatusCode = HttpStatusCode.Created
        };
    }

    public async Task<ReturnModel<Appointment?>> UpdateAsync(UpdateAppointmentRequest request)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
        if (appointment == null)
        {
            throw new NotFoundException("Appointment not found.");
        }

        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        if (doctor == null)
        {
            throw new ValidationException("Doctor not found.");
        }

        if (request.AppointmentDate < DateTime.Now.AddDays(3))
        {
            throw new ValidationException("Appointment date must be at least 3 days from now.");
        }

        _mapper.Map(request, appointment);
        await _appointmentRepository.UpdateAsync(appointment);

        return new ReturnModel<Appointment?>
        {
            Success = true,
            Data = appointment,
            Message = "Appointment is updated.",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ReturnModel<Appointment>> RemoveAsync(Guid id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
        {
            throw new NotFoundException("Appointment not found.");
        }

        await _appointmentRepository.RemoveAsync(appointment);
        return new ReturnModel<Appointment>
        {
            Success = true,
            Data = appointment,
            Message = "Appointment Deleted",
            StatusCode = HttpStatusCode.NoContent
        };
    }
}
