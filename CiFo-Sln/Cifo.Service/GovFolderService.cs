using Cifo.Model.GovFolder;
using Cifo.Service.Interfaces;

namespace Cifo.Service
{
    public class GovFolderService : IGovFolderService
    {
        readonly HttpClient _httpClient;
        readonly GovFolderUrl _govFolder;
        public GovFolderService(GovFolderUrl govFolder)
        {
            _govFolder = govFolder;
            _httpClient = new HttpClient() { BaseAddress = new Uri(_govFolder?.UrlBase) };
        }
        public Task<DocumentDto?> AuthenticateDocument(DocumentDto document)
        {
            throw new NotImplementedException();
        }

        public Task<CitizenDto> RegisterCitizen(CitizenDto citizen)
        {
            throw new NotImplementedException();
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
