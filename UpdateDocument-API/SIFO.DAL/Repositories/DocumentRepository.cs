﻿using CIFO.Models.Models;
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

        public async Task<string> SaveDocument(DocumentModel document)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                var path = document.IdUser.ToString() + "/" + document.DocumentName.Replace(".", "") + guid;
                await _client.SetAsync(path, document);

                return path;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
