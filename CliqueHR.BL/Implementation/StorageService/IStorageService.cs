using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IStorageService
    {
        UploadFileResponse UploadFile(StorageRequest storageRequest);
        FileReadData ReadFileData(string path, bool isPrivate);

        void DeleteFileData(string FileName, bool isPrivate, string CompanyCode);
    }
}
