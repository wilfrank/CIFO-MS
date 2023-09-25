using CIFO.DAL.Repositories;
using CIFO.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Services.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<List<DocumentModel>> GetDocumentsByUserId(int userId)
        {
            return await _documentRepository.GetDocumentsByUserId(userId);
        }
    }
}
