using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class EmployeeGroupModel
    {

    }
    public class EmployeeGroup
    {
        public int Id { get; set; }
        public string EmployeeGroupCode { get; set; }
        public DataTable DTEmpGroupEmpIdMapping { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDoNotUse { get; set; }
    }

    public class EmployeeGroupResponse
    {
        public int Id { get; set; }
        public string EmployeeGroupCode { get; set; }
        public string Employees { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDoNotUse { get; set; }
    }

    public class EmployeeGrid
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string OrgUnit { get; set; }
        public string Location { get; set; }
    }

    public class EmployeeGroupByIdResponse
    {
        public int Id { get; set; }
        public string EmployeeGroupCode { get; set; }
        public PaginationData<EmployeeGrid> EmployeeGrid { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsDoNotUse { get; set; }
    }

    public class EmployeeGroupValidation : AbstractValidator<EmployeeGroup>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public EmployeeGroupValidation()
        {
            this[ValidateAll_key] = ValidateAll;
        }

        private List<ValidationMessage> ValidateAll(EmployeeGroup model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.EmployeeGroupCode))
            {
                message.Add(new ValidationMessage
                {
                    Property = "EmployeeGroup",
                    Message = "Employee group can not be blank."
                });
            }


            return message;
        }
    }
}
