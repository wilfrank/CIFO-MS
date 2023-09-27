using Cifo.Model.Document;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.DAL.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IConfiguration _configuration;
        private string _authSecret;
        private string _basePath;

        public async Task<string> SaveDocument(DocumentModel document)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                var path = document.IdUser.ToString() + "/" + document.DocumentName.Replace(".", "") + guid;
                //await _client.SetAsync(path, document);

                return path;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<bool> UpdateDocument(DocumentModel document)
        {
            try
            {
               // await _client.SetAsync(document.PathBD, document);

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
