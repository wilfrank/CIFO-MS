using Cifo.Model;
using Cifo.Model.Document;
using Cifo.Model.GovFolder;
using Cifo.Service.Interfaces;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Cifo.Service
{
    public class GovFolderService : IGovFolderService
    {
        readonly HttpClient _httpClient;
        readonly GovFolderUrl _govFolder;
        readonly OperatorDto _operator;
        public GovFolderService(GovFolderUrl govFolder, OperatorDto @operator)
        {
            _govFolder = govFolder;
            _httpClient = new HttpClient() { BaseAddress = new Uri(_govFolder?.UrlBase) };
            _operator = @operator;
        }

        public async Task<bool> AuthenticationDocument(AuthenticateDocumentModel document)
        {
            try
            {
                var json = JsonConvert.SerializeObject(document);
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var jsonData = JsonConvert.SerializeObject(body);
                var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var request = await _httpClient.PostAsync(_govFolder.UrlAuthenticateDoc, data);
                var result = await request.Content.ReadAsStringAsync();

                if (result.Contains("Error al"))
                {
                    throw new Exception($"Error autenticar documento - {result}");
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<CitizenDto> RegisterCitizen(CitizenDto citizen)
        {
            citizen.operatorId = _operator.operatorId;
            citizen.operatorName = _operator.operatorName;
            var jsonData = JsonConvert.SerializeObject(citizen);
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(_govFolder.UrlRegister, data);
            var result = await request.Content.ReadAsStringAsync();
            if (result.Contains("Error al"))
            {
                throw new Exception($"Error creating citizen at GovCarpeta Endpoint - {result}");
            }
            return citizen;
        }

        public Task<CitizenDto> UnregisterCitizen(CitizenDto citizenDto)
        {
            throw new NotImplementedException();
        }

        public async Task<string?> ValidateCitizen(string citizenId)
        {
            var request = await _httpClient.GetAsync($"{_govFolder.UrlValidateCtz}/{citizenId}");
            var response = await request.Content.ReadAsStringAsync();
            return response;
        }
      
    }
}
