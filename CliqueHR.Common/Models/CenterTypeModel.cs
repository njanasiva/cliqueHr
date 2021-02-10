using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class CenterTypeModel
    {
        public int Id { get; set; }
        public string CenterTypeName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsDoNotUse { get; set; }
    }
    public class CenterTypeValidation:AbstractValidator<CenterTypeModel>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateAll_key_Id_Name = "ValidateAll_key_Id_Name";
        public CenterTypeValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateAll_key_Id_Name] = ValidateAll_Id_CenterTName;
        }

        private List<ValidationMessage> ValidateAll(CenterTypeModel model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.CenterTypeName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "CentreType",
                    Message = "CentreType can not be blank."
                });
            }
            return message;
        }
        private List<ValidationMessage> ValidateAll_Id_CenterTName(CenterTypeModel model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CentreID",
                    Message = "CentreID can not be Zero."
                });
            }
            if (string.IsNullOrEmpty(model.CenterTypeName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "CentreType",
                    Message = "CentreType can not be blank."
                });
            }
            return message;
        }
    }
}
