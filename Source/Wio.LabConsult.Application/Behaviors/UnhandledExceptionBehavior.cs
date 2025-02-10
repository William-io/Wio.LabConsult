using MediatR;
using Microsoft.Extensions.Logging;

namespace Wio.LabConsult.Application.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError("Solicitação de aplicação: exceção não tratada para solicitação {Name} {@Request}", requestName, request);
            throw new Exception("Solicitação de Aplicação com Erros");
        }
    }
}
