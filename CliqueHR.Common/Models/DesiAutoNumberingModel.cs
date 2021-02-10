using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class DesiAutoNumberingModel
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int AppendNumber { get; set; }
        public DataTable DTAttributesMap { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDoNotUse { get; set; }
    }
    public class DesiAutoNumberingByidModel
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int AppendNumber { get; set; }
        public string Entity { get; set; }
        public string OrgUnit { get; set; }
        public string Departments { get; set; }
        public int EntityId { get; set; }
        public int OrgUnitId { get; set; }
        public int DeptId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDoNotUse { get; set; }
    }
    public class DesiAutoNumberingValidation : AbstractValidator<DesiAutoNumberingModel>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateAllWithId_key = "ValidateAllWithId_key";
        public DesiAutoNumberingValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateAllWithId_key] = ValidateAllWithId;
        }

        private List<ValidationMessage> ValidateAllWithId(DesiAutoNumberingModel model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Id",
                    Message = "Id Can not blank or null"
                });
            }
            if (model.AppendNumber == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Name",
                    Message = "Designation Name Can not blank or null"
                });
            }
            if (model.CreatedBy == 0)
            {
                message.Add(new ValidationMessage
                {
                    Message = "Created By can not be null or Zero",
                    Property = "CreatedBy"
                });
            }
            return message;
        }

        private List<ValidationMessage> ValidateAll(DesiAutoNumberingModel model)
        {
            var message = new List<ValidationMessage>();
            if (model.AppendNumber==0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Name",
                    Message = "Designation Name Can not blank or null"
                });
            }
            if (model.CreatedBy == 0)
            {
                message.Add(new ValidationMessage
                {
                    Message = "Created By can not be null or Zero",
                    Property = "CreatedBy"
                });
            }
            return message;
        }
    }
}
