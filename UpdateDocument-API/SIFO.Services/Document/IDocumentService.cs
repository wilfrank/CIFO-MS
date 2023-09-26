using CIFO.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Services.Document
{
    public interface IDocumentService
    {
        Task<string> SaveDocument(DocumentModel document);
    }
}
