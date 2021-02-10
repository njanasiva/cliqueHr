using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class StorageRequest
    {
        public bool IsPrivate { get; set; }
        public string ContainerName { get; set; }
        public string[] FolderName { get; set; }
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }

    public class StorageRequestValidation : AbstractValidator<StorageRequest>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public StorageRequestValidation()
        {
            this[ValidateAll_key] = ValidateAll;
        }

        private List<ValidationMessage> ValidateAll(StorageRequest model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.ContainerName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "ContainerName",
                    Message = "ContainerName can not be blank."
                });
            }
            if (model.FolderName == null || model.FolderName.Length == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "FolderName",
                    Message = "FolderName can not be blank."
                });
            }
            if (model.FileStream == null)
            {
                message.Add(new ValidationMessage
                {
                    Property = "FileStream",
                    Message = "FileStream can not be null."
                });
            }
            return message;
        }
    }

    public class UploadFileResponse
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContainerName { get; set; }
    }
    public class FileReadData
    {
        public string FileName { get; set; }
        public Stream FileStream { get; set; }
    }
}
