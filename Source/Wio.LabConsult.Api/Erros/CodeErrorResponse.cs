using Newtonsoft.Json;

namespace Wio.LabConsult.Api.Erros;

public class CodeErrorResponse
{
    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string[]? Message { get; set; }

    public CodeErrorResponse(int statusCode, string[]? message = null)
    {
        StatusCode = statusCode;
        if (message is null)
        {
            Message = new string[0];
            var text = GetDefaultMessageStatusCode(statusCode);
            Message[0] = text;
        }
        else
        {
            Message = message;
        }
    }

    private string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A solicitação enviada contém erros",
            401 => "Você não tem autorização para este recurso",
            404 => "O recurso solicitado não foi encontrado",
            500 => "Ocorreram erros no servidor",
            _ => string.Empty
        };
    }
}
