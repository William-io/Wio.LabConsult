namespace Wio.LabConsult.Application.Specifications;

public abstract class SpecificationParams
{
    public string? Sort { get; set; }
    public int PageIndex { get; set; } = 1;
    
    private const int MaxPageSize = 50;

    private int _pageSize = 3;

    public int PageSize
    {
        get => _pageSize;
        //SE O VALOR DE PÁGINA FOR MAIOR QUE O MÁXIMO, O VALOR DE PÁGINA SERÁ O MÁXIMO QUE E 50
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? Search { get; set; }
}
