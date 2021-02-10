using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class GradeType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
<<<<<<< HEAD
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
=======
        public double MinSalary { get; set; }
        public double MaxSalary { get; set; }
>>>>>>> change
        public bool IsDoNotUse { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }

    }


    public class GradeTypeModelValidation : AbstractValidator<GradeType>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";

        public GradeTypeModelValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateEditFields_key] = Validate_EditFields;
        }
        private List<ValidationMessage> ValidateAll(GradeType model)
        {
            var message = new List<ValidationMessage>();
            if (model.TypeName == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "TypeName",
                    Message = "Grade can not be blank."
                });
            }          
            if (model.MinSalary <= 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "MinSalary",
                    Message = "Minimum Salary should be greater then 0."
                });
            }
<<<<<<< HEAD
            if (model.MaxSalary < model.MinSalary)
=======
            if (model.MaxSalary > 1000000)
>>>>>>> change
            {
                message.Add(new ValidationMessage
                {
                    Property = "MaxSalary",
<<<<<<< HEAD
                    Message = "Maximum salary should be greater than Min salary"
=======
                    Message = "Maximum salary less then 10,00,000"
>>>>>>> change
                });
            }
            return message;
        }
        private List<ValidationMessage> Validate_EditFields(GradeType model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "GradeType",
                    Message = "Id can not be Zero."
                });
            }
            if (model.TypeName == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "TypeName",
                    Message = "Grade can not be blank."
                });
            }
            if (model.MinSalary <= 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "MinSalary",
                    Message = "Minimum Salary should be greater then 0."
                });
            }
<<<<<<< HEAD
            if (model.MaxSalary < model.MinSalary)
=======
            if (model.MaxSalary > 1000000)
>>>>>>> change
            {
                message.Add(new ValidationMessage
                {
                    Property = "MaxSalary",
<<<<<<< HEAD
                    Message = "Maximum salary should be greater than Min salary"
=======
                    Message = "Maximum salary less then 10,00,000"
>>>>>>> change
                });
            }
            return message;
        }
    }
}
