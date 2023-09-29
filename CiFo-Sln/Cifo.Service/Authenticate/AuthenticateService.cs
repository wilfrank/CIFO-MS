using Cifo.Model;
using Cifo.Model.Document;
using Cifo.Model.Messages;
using Cifo.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Authenticate
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IGovFolderService _authenticationServices;
        private readonly IUserService _userService;

        public AuthenticateService( IGovFolderService authenticationServices, IUserService userService)
        {
            _authenticationServices = authenticationServices;
            _userService = userService;
        }

        public async Task<UserModel> AuthenticationDocument(AuthenticateDocumentModel document, string userKey)
        {
            try
            {
                UserModel user = new UserModel();

                if (await _authenticationServices.AuthenticationDocument(document))
                {
                    user = await _userService.GetById(userKey);

                    if (user != null)
                    {
                        user.Documents.Where(x => x.Url.Equals(document.UrlDocument)).FirstOrDefault().IsVerified = true;

                        await _userService.CreateAsync(user);

                    }


                }
                return user;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
