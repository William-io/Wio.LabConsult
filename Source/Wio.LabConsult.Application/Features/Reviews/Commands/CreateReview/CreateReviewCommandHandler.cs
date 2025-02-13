using AutoMapper;
using MediatR;
using Wio.LabConsult.Application.Features.Reviews.Queries.Vms;
using Wio.LabConsult.Application.Persistence;
using Wio.LabConsult.Domain.Reviews;

namespace Wio.LabConsult.Application.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewVm>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReviewCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReviewVm> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var reviewEntity = new Review
        {
            Comment = request.Comment,
            Rating = request.Rating,
            Name = request.Name,
            ConsultId = request.ConsultId
        };

        _unitOfWork.Repository<Review>().AddEntity(reviewEntity);
        var resultado = await _unitOfWork.Complete();

        if (resultado <= 0)
        {
            throw new Exception("Comentario não foi salvo!");
        }

        return _mapper.Map<ReviewVm>(reviewEntity);
    }
}