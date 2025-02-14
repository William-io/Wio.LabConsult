using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using Wio.LabConsult.Application.Features.Appointments.VMs;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Appointments;

namespace Wio.LabConsult.Application.Features.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AppointmentVm> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {

        var includes = new List<Expression<Func<Appointment, object>>>();
        includes.Add(p => p.AppointmentItems!.OrderBy(x => x.Consult));

        var appointmentCart = await _unitOfWork.Repository<Appointment>().GetEntityAsync(
            x => x.AppointmentCartMasterId == request.AppointmentId,
            includes,
            true
        );

        if (appointmentCart is null)
        {
            appointmentCart = new Appointment
            {
                AppointmentCartMasterId = request.AppointmentId,
                AppointmentItems = new List<AppointmentItem>()
            };

            _unitOfWork.Repository<Appointment>().AddEntity(appointmentCart);
            await _unitOfWork.Complete();
        }

        return _mapper.Map<AppointmentVm>(appointmentCart);
    }
}
