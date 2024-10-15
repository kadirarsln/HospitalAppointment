using Core.Exceptions;
using Core.Models;
using HospitalAppointment.Models.Dtos.Doctors.Requests;
using HospitalAppointment.Models.Dtos.Doctors.Responses;
using HospitalAppointment.Models.Entities;
using HospitalAppointment.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HospitalAppointment.WebApi.Controllers.DoctorsController;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<ActionResult<ReturnModel<List<DoctorResponseDto>>>> GetAll()
    {
        var result = await _doctorService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnModel<DoctorResponseDto?>>> GetById(int id)
    {
        try
        {
            var result = await _doctorService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ReturnModel<DoctorResponseDto?>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.NotFound
            });
        }
    }

    [HttpPost("add")]
    public async Task<ActionResult<ReturnModel<Doctor?>>> Add([FromBody] CreateDoctorRequest request)
    {
        try
        {
            var result = await _doctorService.AddAsync(request);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ReturnModel<Doctor?>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult<DoctorResponseDto>> UpdateDoctor([FromBody] UpdateDoctorRequest updateDoctorRequest)
    {
        try
        {
            var result = await _doctorService.UpdateAsync(updateDoctorRequest);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ReturnModel<Doctor?>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.NotFound
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ReturnModel<Doctor?>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnModel<Doctor>>> Remove(int id)
    {
        try
        {
            var result = await _doctorService.RemoveAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ReturnModel<Doctor>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.NotFound
            });
        }
    }
    [HttpGet("getdoctorswithappointments")]
    public async Task<ActionResult<ReturnModel<List<DoctorWithAppointmensDto>>>> GetDoctorsWithAppointments()
    {
        var result = await _doctorService.GetDoctorWithAppointmentsAsync();
        return Ok(result);
    }


    [HttpGet("{doctorId}/appointments")]
    public async Task<IActionResult> GetDoctorWithAppointmentsById(int doctorId)
    {
        try
        {
            var result = await _doctorService.GetDoctorWithAppointmentsByIdAsync(doctorId);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = "Doctor not Found" });
        }
    }

}