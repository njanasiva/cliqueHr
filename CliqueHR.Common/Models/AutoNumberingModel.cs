using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class AutoNumberingModel
    {
    }

    public class AutoNumbering
    {
        public string Prefix { get; set; }
        public int AppendNumber { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }

    public class AutoNumberingValidation : AbstractValidator<AutoNumbering>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public AutoNumberingValidation()
        {
            this[ValidateAll_key] = ValidateAll;
        }

        private List<ValidationMessage> ValidateAll(AutoNumbering model)
        {
            var message = new List<ValidationMessage>();

            if (string.IsNullOrEmpty(model.Prefix))
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
