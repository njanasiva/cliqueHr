using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class EmployeeType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public Boolean SelfService { get; set; }
        public int MinAge { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public Boolean IsDoNotUse { get; set; }
    }
    public class EmployeeTypeValidation : AbstractValidator<EmployeeType>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";
        public EmployeeTypeValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateEditFields_key] = Validate_EditFields;
        }

        private List<ValidationMessage> ValidateAll(EmployeeType model)
        {
            var message = new List<ValidationMessage>();
            if (model.TypeName == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "TypeName",
                    Message = "TypeName can not be blank."
                });
            }
           
            if (model.MinAge == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "MinAge",
                    Message = "MinAge can not be 0."
                });
            }
            if (model.MinAge < 18)
            {
                message.Add(new ValidationMessage
                {
                    Property = "MinAge",
                    Message = "MinAge is greter than 18."
                });
            }
            return message;
        }
        private List<ValidationMessage> Validate_EditFields(EmployeeType model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "EmployeeType",
                    Message = "Id can not be Zero."
                });
            }
            if (model.TypeName == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "TypeName",
                    Message = "TypeName can not be blank."
                });
            }

            if (model.MinAge == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "MinAge",
                    Message = "MinAge can not be 0."
                });
            }
            if (model.MinAge < 18)
            {
                message.Add(new ValidationMessage
                {
                    Property = "MinAge",
                    Message = "MinAge is greter than 18."
                });
            }
            return message;
        }
    }
}
