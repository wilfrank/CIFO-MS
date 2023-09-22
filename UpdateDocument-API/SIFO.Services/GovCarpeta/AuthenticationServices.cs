using CIFO.Models.Models;
using Microsoft.Extensions.Configuration;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CIFO.Services.GovCarpeta
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IConfiguration _configuration;
        private string _baseAddress;
        private string _endpoint;


        public AuthenticationServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseAddress = _configuration["Authenticate:BaseAddress"];
            _endpoint = _configuration["Authenticate:Endpoint"];
        }

        public async Task<bool> AuthenticationDocument(AuthenticateDocumentModel document)
        {
            var json = JsonSerializer.Serialize(document);
            var body = new StringContent(json, Encoding.UTF8, "application/json");

            await PutMethod(body);

            return true;
        }

        private async Task<dynamic> PutMethod(StringContent body)
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

                return JsonSerializer.Deserialize<ExpandoObject>(content);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
