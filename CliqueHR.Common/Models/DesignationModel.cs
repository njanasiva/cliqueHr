using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class DesignationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DataTable DTAttributesMap { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDoNotUse { get; set; }
    }
    public class DesignationResponseModel
    {
        public int DesignationID { get; set; }
        public string Designation { get; set; }
        public string DesignationCode { get; set; }
        public string EntityOrgDeptDesc { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDoNotUse { get; set; }
        public List<EntityOrgunitDepartmentModel> entityOrgunitDepartment { get; set; }
    }



    public class DesignationValidation : AbstractValidator<DesignationModel>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateId_key = "ValidateId_key";
        public DesignationValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateId_key] = ValidateId;
        }
        private List<ValidationMessage> ValidateAll(DesignationModel model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.Name))
            {
                message.Add(new ValidationMessage
                {
                    Property = "Name",
                    Message = "Designation Name Can not blank or null"
                });
            }
            if (string.IsNullOrEmpty(model.Code))
            {
                message.Add(new ValidationMessage
                {
                    Message = "Designation Code Can not blank or null",
                    Property = ""
                });
            }
            return message;
        }
        private List<ValidationMessage> ValidateId(DesignationModel model)
        {
            throw new NotImplementedException();
        }

    }
}
