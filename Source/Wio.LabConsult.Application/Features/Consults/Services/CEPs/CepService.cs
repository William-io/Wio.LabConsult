using System.Text.Json;

namespace Wio.LabConsult.Application.Features.Consults.Services.CEPs;

public class CepService
{
    public bool IsCep(string? value)
    {
        return !string.IsNullOrEmpty(value) && value.Length == 8 && int.TryParse(value, out _);
    }

    public async Task<AddressData?> GetAddressFromCepAsync(string? cep)
    {
        if (string.IsNullOrEmpty(cep)) return null;

        using var client = new HttpClient();
        var response = await client.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var addressData = JsonSerializer.Deserialize<AddressData>(response, options);


        if (addressData != null && string.IsNullOrEmpty(addressData.Erro))
            return addressData;

        return null;
    }
}