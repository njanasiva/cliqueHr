using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class AutoNumber
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public int AppendNumber { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDoNotUse { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class AutoNumberValidation : AbstractValidator<AutoNumber>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";
        public AutoNumberValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateEditFields_key] = Validate_EditFields;
        }

        private List<ValidationMessage> ValidateAll(AutoNumber model)
        {
            var message = new List<ValidationMessage>();
            if (model.Prefix == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "Prefix",
                    Message = "Prefix can not be blank."
                });
            }
            

            return message;
        }
        private List<ValidationMessage> Validate_EditFields(AutoNumber model)
        {
            var message = new List<ValidationMessage>();
            if (model.Prefix == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "Prefix",
                    Message = "Prefix can not be blank."
                });
            }
          
          
            return message;
        }
    }

}
