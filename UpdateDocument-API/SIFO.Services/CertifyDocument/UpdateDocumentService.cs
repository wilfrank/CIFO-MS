using CIFO.Models.Models;
using CIFO.Services.GovCarpeta;
using CIFO.Services.Messages;
using CIFO.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Services.CertifyDocument
{
    public class UpdateDocumentService : IUpdateDocumentService
    {
        private readonly IStorageService _storageService;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly INotificationService _notificationService;

        public UpdateDocumentService(IStorageService storageService, 
                                      IAuthenticationServices authenticationServices,
                                      INotificationService notificationService)
        {
            _storageService = storageService;
            _authenticationServices = authenticationServices;
            _notificationService = notificationService;
        }
        public async Task<FileDataDTO> UpdateDocument(FileDataDTO fileDTO)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileDTO.ImageString))
                {
                    byte[] data = Convert.FromBase64String(fileDTO.ImageString);
                    MemoryStream stream = new MemoryStream();
                    stream.Write(data, 0, data.Length);
                    stream.Position = 0;
                    var key = await _storageService.Upload(fileDTO.UserId.GetValueOrDefault(), stream, fileDTO.ContentType, fileDTO.Name);

                    if (!string.IsNullOrEmpty(key))
                    {
                        
                        AuthenticateDocumentModel model = new AuthenticateDocumentModel
                        {
                            idCitizen = fileDTO.UserId,
                            UrlDocument = key,
                            operatorName = "CIFO"
                        };

                        //AuthenticateDocumentModel model = new AuthenticateDocumentModel
                        //{
                        //    idCitizen = fileDTO.UserId,
                        //    UrlDocument = "https://firebasestorage.googleapis.com/v0/b/eafit-cifo.appspot.com/o/1256358953%2FDocumento.PDF_60dcdffe-2f0c-4eaa-a8dd-5b883ad20e19?alt=media&token=f2bfb809-84ab-4112-8bf3-e4ef572bc043",
                        //    operatorName = "CIFO"
                        //};

                        _authenticationServices.AuthenticationDocument(model);

                        _notificationService.SendEmailConfirmation(@"Señor usuario.
                                                                     Su documento esta en proceso de verificación.
                                                                     Se le informará cuando el proceso termine",fileDTO.UserEmail,"Verificación en proceso");

                    }
                    else
                    {
                        throw new Exception("Error retornando la ruta del archivo");
                    }
                    return fileDTO;
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
