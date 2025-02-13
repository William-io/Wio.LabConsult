using MediatR;
using Microsoft.AspNetCore.Http;
using Wio.LabConsult.Application.Features.Consults.Commands.CreateConsult;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;

namespace Wio.LabConsult.Application.Features.Consults.Commands.UpdateConsult;

public class UpdateConsultCommand : IRequest<ConsultVm>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Employee { get; set; }
    public int Availability { get; set; }
    public string? CategoryId { get; set; }

    public IReadOnlyList<IFormFile>? Photos { get; set; }

    public IReadOnlyList<CreateConsultImageCommand>? ImageUrls { get; set; }
}