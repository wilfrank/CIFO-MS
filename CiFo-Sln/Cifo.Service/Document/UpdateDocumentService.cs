using Cifo.Model;
using Cifo.Model.Document;
using Cifo.Service.Interfaces;
using Cifo.Service.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifo.Service.Document
{
    public class UpdateDocumentService : IUpdateDocumentService
    {
        private readonly IStorageService _storageService;
        private readonly IDocumentService _documentService;
        private readonly IGovFolderService _authenticationServices;
        private readonly IUserService _userService;
        private readonly IFirebaseAuthService _firebaseAuth;

        public UpdateDocumentService(IStorageService storageService,
                                      IDocumentService documentService,
                                      IGovFolderService authenticationServices,
                                      IUserService userService,
                                      IFirebaseAuthService firebaseAuthService)
        {
            _storageService = storageService;
            _documentService = documentService;
            _authenticationServices = authenticationServices;
            _userService = userService;
            _firebaseAuth = firebaseAuthService;
        }
        public async Task<UserModel> UpdateDocument(FileDataDTO fileDTO, string userKey)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileDTO.ImageString))
                {
                    UserModel user = new UserModel();
                    byte[] data = Convert.FromBase64String(fileDTO.ImageString);
                    MemoryStream stream = new MemoryStream();
                    stream.Write(data, 0, data.Length);
                    stream.Position = 0;

                    var key = await _storageService.Upload(fileDTO.UserId.GetValueOrDefault(), stream, fileDTO.ContentType, fileDTO.Name);
                    //var key = "https://firebasestorage.googleapis.com/v0/b/eafit-cifo.appspot.com/o/1256358953%2FDocumento.PDF_60dcdffe-2f0c-4eaa-a8dd-5b883ad20e19?alt=media&token=f2bfb809-84ab-4112-8bf3-e4ef572bc043";
                    if (!string.IsNullOrEmpty(key))
                    {
                        DocumentDto document = new DocumentDto
                        {
                            Name = fileDTO.Name,
                            IsVerified = false,
                            Label = fileDTO.Name,
                            Url = key
                        };

                         user = await _userService.GetById(userKey);

                        user.Documents.Add(document);

                        await _userService.CreateAsync(user);
                    }
                    else
                    {
                        throw new Exception("Error retornando la ruta del archivo");
                    }
                    return user;
                }
                else
                {
                    throw new Exception("El archivo es obligatorio.");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
