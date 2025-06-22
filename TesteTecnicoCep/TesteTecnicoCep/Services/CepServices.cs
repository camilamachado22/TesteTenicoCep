using System.Text.Json;

namespace TesteTecnicoCep.Services
{
    public class CepService
    {
        private readonly HttpClient _httpClient;

        public CepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(string ?logradouro, string? localidade, string? complemento)> BuscarEnderecoPorCep(string cep)
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var dados = JsonSerializer.Deserialize<CepResponse>(json);

            return (dados.logradouro, dados.localidade, dados.complemento);
        }
    }

    public record CepResponse(string cep,string logradouro, string localidade, string cidade, string complemento);
}
