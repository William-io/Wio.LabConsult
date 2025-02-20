﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Wio.LabConsult.Application.Features.Consults.Queries.VMs;

namespace Wio.LabConsult.Application.Features.Consults.Commands.CreateConsult;

public class CreateConsultCommand : IRequest<ConsultVm>
{
    public string? Name { get; set; }
    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string? Employee { get; set; }

    public int Availability { get; set; }

    public string? Crm { get; set; }

    public string? CategoryId { get; set; }

    public IReadOnlyList<IFormFile>? Photos { get; set; }

    public IReadOnlyList<CreateConsultImageCommand>? ImageUrls { get; set; }

    public string? Local { get; set; }
}