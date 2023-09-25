using CIFO.DAL.Repositories;
using CIFO.Models.Models;
using CIFO.Services.GovCarpeta;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> SaveDocument(DocumentModel document)
        {
            return await _documentRepository.SaveDocument(document);
        }
    }
}
