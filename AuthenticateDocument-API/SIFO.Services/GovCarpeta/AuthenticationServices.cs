using CIFO.Models.Models;
using Microsoft.Extensions.Configuration;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net;
using CIFO.DAL.Repositories;

namespace CIFO.Services.GovCarpeta
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IConfiguration _configuration;
        private readonly IDocumentRepository _documentRepository;
        private string _baseAddress;
        private string _endpoint;


        public AuthenticationServices(IConfiguration configuration, IDocumentRepository documentRepository)
        {
            _configuration = configuration;
            _documentRepository = documentRepository;
            _baseAddress = _configuration["GovCarpeta:BaseAddress"];
            _endpoint = _configuration["GovCarpeta:Endpoint"];
        }

        public async Task<bool> AuthenticationDocument(AuthenticateDocumentCompleteModel document)
        {
            try
            {
                var json = JsonSerializer.Serialize(document.AuthenticateModel);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                if (await PutMethod(body))
                {
                    document.DocumentModel.Status = "Autenticado";

                    await _documentRepository.UpdateDocument(document.DocumentModel);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private async Task<bool> PutMethod(StringContent body)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(_baseAddress);

                var response = await client.PutAsync(_endpoint, body);
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Request error on: {_endpoint}");

                var content = await response.Content.ReadAsStringAsync();

                client.Dispose();

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
