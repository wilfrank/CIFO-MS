﻿using CIFO.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Services.CertifyDocument
{
    public interface ICertifyDocumentService
    {
        Task<FileDataDTO> CertifyDocument(FileDataDTO fileDTO);
    }
}
