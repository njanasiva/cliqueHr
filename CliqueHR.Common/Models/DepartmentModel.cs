using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class DepartmentModel
    {
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ParentDepartmentId { get; set; }
        public int HOD { get; set; }
        public bool IsDoNotUse { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }

    public class DepartmentValidation : AbstractValidator<Department>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public DepartmentValidation()
        {
            this[ValidateAll_key] = ValidateAll;
        }

        private List<ValidationMessage> ValidateAll(Department model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.Name))
            {
                message.Add(new ValidationMessage
                {
                    Property = "DepartmentName",
                    Message = "Department name can not be blank."
                });
            }

            if (string.IsNullOrEmpty(model.Code))
            {
                message.Add(new ValidationMessage
                {
                    Property = "DepartmentCode",
                    Message = "Department code can not be blank."
                });
            }


            return message;
        }
    }

}
