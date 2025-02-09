using System.Runtime.Serialization;

namespace Wio.LabConsult.Domain.Requests;

public enum RequestStatus
{
    [EnumMember(Value = "Pendente")] 
    Pending,
    [EnumMember(Value = "Pagamento recebido")] 
    Approved,
    [EnumMember(Value = "Agendado")] 
    Completed,
    [EnumMember(Value = "Pagagamento rejeitado")] 
    Rejected
}
