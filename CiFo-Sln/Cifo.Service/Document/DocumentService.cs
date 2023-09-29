using Cifo.Model;
using Cifo.Model.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Document
{
    public class DocumentService : IDocumentService
    {
        public DocumentService()
        {
            
        }
        public async Task<string> SaveDocument(DocumentDto document)
        {
            //return await _documentRepository.SaveDocument(document);
            return "";
        }
    }
}
