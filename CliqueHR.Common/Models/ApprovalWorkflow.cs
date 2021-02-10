using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class ApprovalWorkflow
    {
        public string PathName { get; set; }
        public int Levels { get; set; }
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsDoNotUse { get; set; }
        public DataTable ApprovalPathMap { get; set; }
        public DataTable WorkflowLevelMap { get; set; }
        public DataTable WorkflowLevelApprover { get; set; }

    }


    public class ApprovalPathType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }

    public class WorkflowLevel
    {
        public int Level { get; set; }
        public int FollowUpAfterDays { get; set; }
        public int EscalateTo { get; set; }
        public int EscalateAfterDays { get; set; }
        public List<Approver> ApproverList { get; set; }
        public bool IsDoNotUse { get; set; }
    }

    public class Approver
    {
        public int Id { get; set; }
        public int ApproverId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }


    public class ApprovalWorkflowValidation : AbstractValidator<ApprovalWorkflow>
    {

        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";

        public ApprovalWorkflowValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateEditFields_key] = Validate_EditFields;
        }

        private List<ValidationMessage> ValidateAll(ApprovalWorkflow model)
        {
            var message = new List<ValidationMessage>();

            if (model.PathName == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "PathName",
                    Message = "PathName can not be blank."
                });
            }

            if (model.Levels == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Levels",
                    Message = "Levels can not be 0."
                });
            }

            if (model.Levels > 10)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Levels",
                    Message = "Levels is not greater then 10"
                });
            }

            


            return message;
        }

        private List<ValidationMessage> Validate_EditFields(ApprovalWorkflow model)
        {
            var message = new List<ValidationMessage>();


            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Id",
                    Message = "Id can not be Zero."
                });
            }

            if (model.PathName == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "PathName",
                    Message = "PathName can not be blank."
                });
            }

            if (model.Levels == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Levels",
                    Message = "Levels can not be 0."
                });
            }

            if (model.Levels > 10)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Levels",
                    Message = "Levels is not greater then 10"
                });
            }


            return message;
        }
    }

}
