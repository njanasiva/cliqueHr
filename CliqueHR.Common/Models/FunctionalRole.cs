using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class FunctionalRole
    {
        public int Id { get; set; }
        public string FRoleName { get; set; }
        public string FRoleCode { get; set; }
        public string FRoleDesc { get; set; }
        public string AttachmentFile { get; set; }
        public bool IsDoNotUse { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }

    public class FunctionalRoleValidation : AbstractValidator<FunctionalRole>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";
        public FunctionalRoleValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateEditFields_key] = Validate_EditFields;
        }
        private List<ValidationMessage> ValidateAll(FunctionalRole model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.FRoleCode))
            {
                message.Add(new ValidationMessage
                {
                    Property = "FRoleCode",
                    Message = "FRoleCode can not be blank."
                });
            }
            if (string.IsNullOrEmpty(model.FRoleName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "FRoleName",
                    Message = "FRoleName can not be blank."
                });
            }
            return message;
        }
        private List<ValidationMessage> Validate_EditFields(FunctionalRole model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "RoleId",
                    Message = "Id can not be Zero."
                });
            }
            if (string.IsNullOrEmpty(model.FRoleCode))
            {
                message.Add(new ValidationMessage
                {
                    Property = "FRoleCode",
                    Message = "FRoleCode can not be blank."
                });
            }
            if (string.IsNullOrEmpty(model.FRoleName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "FRoleName",
                    Message = "FRoleName can not be blank."
                });
            }
            return message;
        }
    }
}
