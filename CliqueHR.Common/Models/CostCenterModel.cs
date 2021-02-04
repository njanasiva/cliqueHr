using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class CostCenterModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Code { get; set; }
        public int Head { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsDoNotUse { get; set; }
    }
    public class CostCenterValidation:AbstractValidator<CostCenterModel>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateAll_key_Id = "ValidateAll_key_Id";
        public CostCenterValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateAll_key_Id] = ValidateAll_Id;
        }

        private List<ValidationMessage> ValidateAll_Id(CostCenterModel model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Costid",
                    Message = "Costid can not be blank."
                });
            }
            if (string.IsNullOrEmpty(model.TypeName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "CostTypeName",
                    Message = "CostTypeName can not be blank."
                });
            }
            if (string.IsNullOrEmpty(model.Code))
            {
                message.Add(new ValidationMessage
                {
                    Property = "CostCenterCode",
                    Message = "CostCenterCode can not be blank."
                });
            }
            if (model.Head == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CostCenterHead",
                    Message = "CostCenterHead can not be blank."
                });
            }
            return message;
        }

        private List<ValidationMessage> ValidateAll(CostCenterModel model)
        {
            var message = new List<ValidationMessage>();
            if(string.IsNullOrEmpty(model.TypeName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "CostTypeName",
                    Message = "CostTypeName can not be blank."
                });
            }
            if(string.IsNullOrEmpty(model.Code))
            {
                message.Add(new ValidationMessage
                {
                    Property = "CostCenterCode",
                    Message = "CostCenterCode can not be blank."
                });
            }
            if(model.Head==0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CostCenterHead",
                    Message = "CostCenterHead can not be blank."
                });
            }
            return message;
        }
    }
}
