using CIFO.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.DAL.Repositories
{
    public interface IDocumentRepository
    {
        Task<bool> UpdateDocument(DocumentModel document);

    }
}
