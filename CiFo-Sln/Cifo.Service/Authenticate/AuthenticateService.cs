using Cifo.Model.Document;
using Cifo.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Authenticate
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IGovFolderService _authenticationServices;

        public AuthenticateService( IGovFolderService authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        public async Task<bool> AuthenticationDocument(AuthenticateDocumentModel document, string userKey)
        {
            await _authenticationServices.AuthenticationDocument(document);
            return true;
        }
    }
}
