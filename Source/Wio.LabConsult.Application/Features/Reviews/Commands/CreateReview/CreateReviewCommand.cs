using MediatR;
using Wio.LabConsult.Application.Features.Reviews.Queries.Vms;

namespace Wio.LabConsult.Application.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommand : IRequest<ReviewVm>
{
    public int ConsultId { get; set; }

    public string? Name { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }
}