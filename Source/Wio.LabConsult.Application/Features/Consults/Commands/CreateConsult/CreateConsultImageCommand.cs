namespace Wio.LabConsult.Application.Features.Consults.Commands.CreateConsult;

public class CreateConsultImageCommand
{
    public string? Url { get; set; }
    public int ConsultId { get; set; }
    public string? PublicCode { get; set; }
}