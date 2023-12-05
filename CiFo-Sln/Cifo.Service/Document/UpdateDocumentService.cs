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
        private readonly IUserService _userService;

        public UpdateDocumentService(IStorageService storageService,
                                      IUserService userService)
        {
            _storageService = storageService;
            _userService = userService;
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

                        if (user != null)
                        {
                            user.Documents.Add(document);

                            await _userService.CreateAsync(user);
                        }
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
        public async Task<UserModel> DeleteDocument(string url, string userKey)
        {
            try
            {
                var user = await _userService.GetById(userKey);

                if (user != null)
                {
                    var document=user.Documents.Where(x => x.Url.Equals(url)).FirstOrDefault();

                    if (document != null) 
                    {
                        user.Documents.Remove(document);
                        await _userService.CreateAsync(user);
                    }
                }

                return user;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
