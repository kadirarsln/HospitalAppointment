using AutoMapper;
using Core.Exceptions;
using Core.Models;
using HospitalAppointment.DataAccess.Abstracts;
using HospitalAppointment.Models.Dtos.Doctors.Requests;
using HospitalAppointment.Models.Dtos.Doctors.Responses;
using HospitalAppointment.Models.Entities;
using HospitalAppointment.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HospitalAppointment.Service.Concrete;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public DoctorService(IMapper mapper, IDoctorRepository doctorRepository)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
    }

    public async Task<ReturnModel<List<DoctorResponseDto>>> GetAllAsync()
    {
        var doctors = await _doctorRepository.GetAllAsync();
        var doctorDtos = _mapper.Map<List<DoctorResponseDto>>(doctors);

        return new ReturnModel<List<DoctorResponseDto>>
        {
            Success = true,
            Data = doctorDtos,
            Message = "Doctors Listed.",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ReturnModel<DoctorResponseDto?>> GetByIdAsync(int id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);
        if (doctor == null)
        {
            throw new NotFoundException("Doctor not found.");
        }

        var doctorDto = _mapper.Map<DoctorResponseDto>(doctor);
        return new ReturnModel<DoctorResponseDto?>
        {
            Success = true,
            Data = doctorDto,
            Message = "Doctor is found",
            StatusCode = HttpStatusCode.OK
        };
    }


    public async Task<ReturnModel<Doctor?>> AddAsync(CreateDoctorRequest request)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new ValidationException("Doctor name cannot be empty.");
        }

        var doctor = _mapper.Map<Doctor>(request);
        await _doctorRepository.AddAsync(doctor);

        return new ReturnModel<Doctor?>
        {
            Success = true,
            Data = doctor,
            Message = "Doktor is Added.",
            StatusCode = HttpStatusCode.Created
        };
    }

    public async Task<ReturnModel<Doctor?>> UpdateAsync(UpdateDoctorRequest request)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.Id);
        if (doctor == null)
        {
            throw new NotFoundException("Doctor not found.");
        }

        if (string.IsNullOrEmpty(request.Name))
        {
            throw new ValidationException("Doctor name cannot be empty.");
        }

        _mapper.Map(request, doctor);
        await _doctorRepository.UpdateAsync(doctor);

        return new ReturnModel<Doctor?>
        {
            Success = true,
            Data = doctor,
            Message = "Doktor is Updated.",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<ReturnModel<Doctor>> RemoveAsync(int id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);
        if (doctor == null)
        {
            throw new NotFoundException("Doctor not found.");
        }

        await _doctorRepository.RemoveAsync(doctor);
        return new ReturnModel<Doctor>
        {
            Success = true,
            Data = doctor,
            Message = "Doctor Deleted",
            StatusCode = HttpStatusCode.NoContent
        };
    }

    public async Task<ReturnModel<List<DoctorWithAppointmensDto>>> GetDoctorWithAppointmentsAsync()
    {
        var doctor = await _doctorRepository.GetDoctorWithAppointments().ToListAsync();
        var doctorAsDto = _mapper.Map<List<DoctorWithAppointmensDto>>(doctor);

        return new ReturnModel<List<DoctorWithAppointmensDto>>
        {
            Success = true,
            Data = doctorAsDto,
            Message = "Doctors with appointments are listed.",
            StatusCode = HttpStatusCode.OK
        };

    }

    public async Task<DoctorWithAppointmensDto> GetDoctorWithAppointmentsByIdAsync(int doctorId)
    {
        var doctor = await _doctorRepository.GetDoctorWithAppointmentsByIdAsync(doctorId);

        if (doctor == null)
        {
            throw new NotFoundException("Doctor not found.");
        }

        var doctorAsDto = _mapper.Map<DoctorWithAppointmensDto>(doctor);

        return doctorAsDto;
    }
}
