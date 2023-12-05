using Cifo.Model;
using Cifo.Model.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Authenticate
{
    public interface IAuthenticateService
    {
        Task<UserModel> AuthenticationDocument(AuthenticateDocumentModel document,string userKey);
    }
}
