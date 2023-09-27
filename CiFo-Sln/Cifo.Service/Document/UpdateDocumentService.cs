﻿using Cifo.Model.Document;
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

        public UpdateDocumentService(IStorageService storageService,
                                      IDocumentService documentService,
                                      IGovFolderService authenticationServices)
        {
            _storageService = storageService;
            _documentService = documentService;
            _authenticationServices = authenticationServices;
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
                    //var key = "https://firebasestorage.googleapis.com/v0/b/eafit-cifo.appspot.com/o/1256358953%2FDocumento.PDF_60dcdffe-2f0c-4eaa-a8dd-5b883ad20e19?alt=media&token=f2bfb809-84ab-4112-8bf3-e4ef572bc043";
                    if (!string.IsNullOrEmpty(key))
                    {
                        DocumentModel document = new DocumentModel
                        {
                            IdUser = fileDTO.UserId,
                            DocumentName = fileDTO.Name,
                            Status = "Cargado",
                            URL = key
                        };

                        var path = await _documentService.SaveDocument(document);

                        document.PathBD = path;

                        AuthenticateDocumentModel model = new AuthenticateDocumentModel
                        {
                            idCitizen = fileDTO.UserId,
                            UrlDocument = key,
                            documentTitle = fileDTO.Name.Replace(".", "")
                        };

                        AuthenticateDocumentCompleteModel modelAut = new AuthenticateDocumentCompleteModel
                        {
                            AuthenticateModel = model,
                            DocumentModel = document
                        };
                        try
                        {
                            await _authenticationServices.AuthenticationDocument(modelAut);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error al autenticar, documento cargado sin autenticar"); 
                        }
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
