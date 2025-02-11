namespace Wio.LabConsult.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) não foi encontrado")
    {
    }
}
