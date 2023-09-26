using CIFO.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.Extensions.Configuration;

namespace CIFO.DAL.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IConfiguration _configuration;
        private string _authSecret;
        private string _basePath;

        IFirebaseClient _client;

        public DocumentRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            _authSecret = _configuration["DataBase:AuthSecret"];
            _basePath = _configuration["DataBase:BasePath"];

            IFirebaseConfig _config = new FirebaseConfig
            {
                AuthSecret = _authSecret,
                BasePath = _basePath
            };

            _client = new FireSharp.FirebaseClient(_config);
        }
        public async Task<List<DocumentModel>> GetDocumentsByUserId(int userId)
        {
            try
            {
                List<DocumentModel> documents = new List<DocumentModel>();

                var responce = await _client.GetAsync(userId.ToString());
                var result = responce.ResultAs<Dictionary<string, DocumentModel>>();

                foreach (var item in result)
                {
                    DocumentModel val = new DocumentModel
                    {
                        IdUser = userId,
                        DocumentName = item.Value.DocumentName,
                        Status = item.Value.Status,
                        URL = item.Value.URL
                    };

                    documents.Add(val);
                }

                return documents;
            }
            catch (Exception ex)
            {

                throw;
            }


        }
    }
}
