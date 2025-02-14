using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Application.Specifications.Requests;

public class RequestForCountingSpecification : BaseSpecification<Request>
{
    public RequestForCountingSpecification(RequestSpecificationParams requestParams)
        : base(x => (string.IsNullOrEmpty(requestParams.Username)
            || x.PatientName!.Contains(requestParams.Username)) &&
            (!requestParams.Id.HasValue || x.Id == requestParams.Id))
    { }
}
