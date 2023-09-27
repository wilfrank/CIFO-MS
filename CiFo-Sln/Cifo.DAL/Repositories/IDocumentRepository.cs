using Cifo.Model.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.DAL.Repositories
{
    public interface IDocumentRepository
    {
        Task<string> SaveDocument(DocumentModel document);
        Task<bool> UpdateDocument(DocumentModel document);
    }
}
