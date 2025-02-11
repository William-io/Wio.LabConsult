namespace Wio.LabConsult.Application.Features.Reviews.Queries.Vms;

public class ReviewVm
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public int ConsultId { get; set; }
}
