using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wio.LabConsult.Application.Features.Appointments.Commands.DeleteAppointmentItem;
using Wio.LabConsult.Application.Features.Appointments.Commands.UpdateAppointmentCart;
using Wio.LabConsult.Application.Features.Appointments.Queries.GetAppointmentById;
using Wio.LabConsult.Application.Features.Appointments.VMs;

namespace Wio.LabConsult.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AppointmentController : ControllerBase
{
    private IMediator _mediator;

    public AppointmentController(IMediator mediator) => _mediator = mediator;

    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetAppointment")]
    [ProducesResponseType(typeof(AppointmentVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AppointmentVm>> GetAppointment(Guid id)
    {
        var appointmentId = id == Guid.Empty ? Guid.NewGuid() : id;
        var query = new GetAppointmentByIdQuery(appointmentId);
        return await _mediator.Send(query);
    }

    [AllowAnonymous]
    [HttpPut("{id}", Name = "UpdateAppointment")]
    [ProducesResponseType(typeof(AppointmentVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AppointmentVm>> UpdateAppointment(Guid id, UpdateAppointmentCommand request)
    {
        request.AppointmentId = id;
        return await _mediator.Send(request);

    }

    [AllowAnonymous]
    [HttpDelete("item/{id}", Name = "DeleteAppointment")]
    [ProducesResponseType(typeof(AppointmentVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AppointmentVm>> DeleteAppointment(int id)
    {
        return await _mediator.Send(new DeleteAppointmentCommand() { Id = id });
    }
}
