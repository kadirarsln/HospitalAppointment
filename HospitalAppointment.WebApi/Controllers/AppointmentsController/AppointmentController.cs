using Core.Exceptions;
using Core.Models;
using HospitalAppointment.Models.Dtos.Appointments.Requests;
using HospitalAppointment.Models.Dtos.Appointments.Responses;
using HospitalAppointment.Models.Entities;
using HospitalAppointment.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HospitalAppointment.WebApi.Controllers.AppointmentController;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<ActionResult<ReturnModel<List<AppointmentResponseDto>>>> GetAll()
    {
        var result = await _appointmentService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnModel<AppointmentResponseDto?>>> GetById(Guid id)
    {
        try
        {
            var result = await _appointmentService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ReturnModel<AppointmentResponseDto?>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.NotFound
            });
        }
    }

    [HttpPost("add")]
    public async Task<ActionResult<ReturnModel<Appointment?>>> Add([FromBody] CreateAppointmentRequest request)
    {
        try
        {
            var result = await _appointmentService.AddAsync(request);
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ReturnModel<Appointment?>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
    }

    [HttpPut("update")]
    public async Task<ActionResult<ReturnModel<Appointment?>>> Update([FromBody] UpdateAppointmentRequest request)
    {
        try
        {
            var result = await _appointmentService.UpdateAsync(request);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ReturnModel<Appointment?>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.NotFound
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ReturnModel<Appointment?>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.BadRequest
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnModel<Appointment>>> Remove(Guid id)
    {
        try
        {
            var result = await _appointmentService.RemoveAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ReturnModel<Appointment>
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                StatusCode = HttpStatusCode.NotFound
            });
        }
    }
}
