using System.Runtime.Serialization;

namespace Wio.LabConsult.Domain.Consults;

public enum ConsultStatus
{
    [EnumMember(Value = "Consulta inativa")]
    Inactive,
    [EnumMember(Value = "Consulta ativa")]
    Active
}
