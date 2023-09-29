using Cifo.Model;
using Cifo.Model.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Document
{
    public interface IDocumentService
    {
        Task<string> SaveDocument(DocumentDto document);
    }
}
