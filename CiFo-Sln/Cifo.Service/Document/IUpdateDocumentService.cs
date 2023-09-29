using Cifo.Model;
using Cifo.Model.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Document
{
    public interface IUpdateDocumentService
    {
        Task<UserModel> UpdateDocument(FileDataDTO fileDTO,string userKey);
    }
}
