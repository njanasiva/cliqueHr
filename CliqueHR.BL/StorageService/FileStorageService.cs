using CliqueHR.Common.Models;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public class FileStorageService : IStorageService
    {
        private StorageRequestValidation storageRequestValidation;
        private string destinationFolder;
        private string directorySeperator;
        public FileStorageService()
        {
            storageRequestValidation = new StorageRequestValidation();
            directorySeperator = Path.DirectorySeparatorChar.ToString();
            destinationFolder = Convert.ToString(ConfigurationManager.AppSettings["StorageLocation"]) ?? Directory.GetCurrentDirectory();
        }
        public FileReadData ReadFileData(string path, bool isPrivate)
        {
            FileReadData fileReadData = new FileReadData();
            var validationResponse = Validator.GetValidationResponseInstance();
            FileStream fileStream = null;
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    validationResponse.Messages.Add(new ValidationMessage { Message = "Path can not be empty", Property = "file" });
                    throw new ValidationException(validationResponse);
                }
                string fileName = string.Empty;
                var folder = path.Split(Convert.ToChar(directorySeperator));
                fileName = folder[folder.Length - 1];

                if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
                {
                    validationResponse.Messages.Add(new ValidationMessage { Message = "Invalid file name.", Property = "file" });
                    throw new ValidationException(validationResponse);
                }
                var accessFolder = directorySeperator + (isPrivate ? "Private" + directorySeperator : "Public" + directorySeperator);
                if (!File.Exists(this.destinationFolder + accessFolder + path))
                {
                    validationResponse.Messages.Add(new ValidationMessage { Message = "file not exists", Property = "file" });
                    throw new ValidationException(validationResponse);
                }
                fileStream = File.OpenRead(this.destinationFolder + accessFolder + path);
                fileReadData.FileName = fileName;
                fileReadData.FileStream = fileStream;
                return fileReadData;
            }
            catch (Exception ex)
            {
                if (fileStream != null)
                    fileStream.Dispose();

                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public UploadFileResponse UploadFile(StorageRequest storageRequest)
        {
            try
            {
                string destinationFolderPath = directorySeperator;
                string accessFolder = string.Empty;
                var validationResponse = storageRequestValidation.Validate(StorageRequestValidation.ValidateAll_key, storageRequest, "Request can not be null");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                var folderName = storageRequest.FolderName;
                accessFolder = directorySeperator + (storageRequest.IsPrivate ? "Private" + directorySeperator : "Public" + directorySeperator);
                if (folderName != null && folderName.Length != 0)
                {
                    destinationFolderPath += string.Join(directorySeperator, folderName) + directorySeperator;
                }
                var destinationFullPath = this.destinationFolder + accessFolder + storageRequest.ContainerName + destinationFolderPath;
                if (!Directory.Exists(destinationFullPath))
                {
                    Directory.CreateDirectory(destinationFullPath);
                }
                Random random = new Random();
                var val = random.Next(100000);
                var file_name = Path.GetFileNameWithoutExtension(storageRequest.FileName) + val + Path.GetExtension(storageRequest.FileName);
                string path = destinationFullPath + file_name;
                using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
                {
                    storageRequest.FileStream.CopyTo(outputFileStream);
                }
                return new UploadFileResponse { ContainerName = storageRequest.ContainerName, FileName = file_name, FilePath = destinationFolderPath };
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void DeleteFileData(string FileName, bool isPrivate, string CompanyCode)
        {
            try
            {
                string destinationFolderPath = directorySeperator;
                string accessFolder = string.Empty;
                if (!string.IsNullOrEmpty(FileName))
                {
                    accessFolder = directorySeperator + (isPrivate ? "Private" + directorySeperator : "Public" + directorySeperator);
                    if (FileName != null && FileName.Length != 0)
                    {
                        destinationFolderPath += string.Join(directorySeperator, FileName);
                    }
                    var destinationFullPath = this.destinationFolder + accessFolder + CompanyCode + destinationFolderPath;
                    if (File.Exists(destinationFullPath))
                    {
                        File.Delete(destinationFullPath);
                    }
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
    }
}
