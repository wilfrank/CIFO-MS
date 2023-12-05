using CIFO.Models.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Services.GovCarpeta
{
    public interface IAuthenticationServices
    {
        Task<bool> AuthenticationDocument(AuthenticateDocumentCompleteModel document);
    }
}
