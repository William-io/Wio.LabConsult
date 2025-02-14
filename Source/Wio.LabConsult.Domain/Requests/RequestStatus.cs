using System.Runtime.Serialization;

namespace Wio.LabConsult.Domain.Requests;

public enum RequestStatus
{
    [EnumMember(Value = "Pendente")]
    Pending,
    [EnumMember(Value = "Agendado")]
    Scheduled,
    [EnumMember(Value = "Confirmado")]
    Confirmed,
    [EnumMember(Value = "Cancelado")]
    Canceled,
    [EnumMember(Value = "Erro no pagamento")]
    Error
    //FALTA IMPLEMENTAR FALTOU E REMARCAR
}
