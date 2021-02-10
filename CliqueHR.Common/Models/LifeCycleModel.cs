using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class LifeCycleModel
    {
    }

    public class Probation
    {
        public int UserId { get; set; }
        public string ProbationName { get; set; }
        public int ProbationPeriod { get; set; }
        public bool AllowExtension { get; set; }
        public int NumberExtensionsAllowed { get; set; }
        public int ExtendProbationDay { get; set; }
        public int IncrementExtensionDay { get; set; }
        public int ConfirmationDay { get; set; }
        public bool IsDoNotUse { get; set; }
        public string Entity { get; set; }
        public string OrgUnit { get; set; }
        public string Department { get; set; }
        public string CentreType { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string EmployeeType { get; set; }
        public string Position { get; set; }
        public string CostCentre { get; set; }
        public string Grade { get; set; }

        public int Action { get; set; }
        public int ProbationDetailId { get; set; }

        public string PublishTo { get; set; }
        public bool AssessmentRequired { get; set; }
        public int AssessmentFormId { get; set; }
        public int Success { get; set; }

        //  public string CreatedDate { get; set; }


    }
    public class ProbationValidation : AbstractValidator<Probation>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";
        public ProbationValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateEditFields_key] = Validate_EditFields;
        }
        private List<ValidationMessage> ValidateAll(Probation model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.ProbationName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "TypeName",
                    Message = "TypeName can not be blank."
                });
            }
            return message;
        }
        private List<ValidationMessage> Validate_EditFields(Probation model)
        {
            var message = new List<ValidationMessage>();
            if (model.ProbationDetailId == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CourseTypeId",
                    Message = "Id can not be Zero."
                });
            }
            if (string.IsNullOrEmpty(model.ProbationName))
            {
                message.Add(new ValidationMessage
                {
                    Property = "TypeName",
                    Message = "TypeName can not be blank."
                });
            }
            return message;
        }
    }
    public class LifeCycleMovement
    {
        public int UserId { get; set; }
        public string MovementReason { get; set; }
        public bool AllowManagerInitiate { get; set; }
        public string AllowRelocationExpense { get; set; }
        public string EditableFieldsId { get; set; }
        public bool IsDoNotUse { get; set; }
        public int ActionId { get; set; }
        public int MovementId { get; set; }
        public int Success { get; set; }
        public int Start { get; set; }
        public int NoofData { get; set; }
        public string MovementField { get; set; }
        public int Total { get; set; }
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public string SearchText { get; set; }
        public bool AllowRoleHolder { get; set; }
        public bool AllowManager { get; set; }
    }
    public class NoticePeriod
    {
        public int UserId { get; set; }
        public int Action { get; set; }
        public string NoticePeriodName { get; set; }
        public int NoticePeriodDays { get; set; }
        public string Entity { get; set; }
        public string OrgUnit { get; set; }
        public string Department { get; set; }
        public string CentreType { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string EmployeeType { get; set; }
        public string Position { get; set; }
        public string CostCentre { get; set; }
        public string Grade { get; set; }
        public bool IsDoNotUse { get; set; }
        public bool IsActive { get; set; }
        public int ConfirmationDays { get; set; }
        public int NoticePeriodDetail { get; set; }

    }
    public class NoticePeriodDetail
    {
        public int UserId { get; set; }
        public int Start { get; set; }
        public int NoofData { get; set; }
        public int ActionId { get; set; }
        public int NoticePeriodDetailId { get; set; }
        public string Entity { get; set; }
        public string OrgUnit { get; set; }
        public string Department { get; set; }
        public string CentreType { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string EmployeeType { get; set; }
        public string Position { get; set; }
        public string CostCentre { get; set; }
        public string Grade { get; set; }
        public bool IsDoNotUse { get; set; }
        public string NoticePeriodName { get; set; }
        public int NoticePeriodDays { get; set; }
        public int ConfirmationDays { get; set; }
        public string PublishTo { get; set; }
        public int Success { get; set; }
        public int Total { get; set; }
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public string SearchText { get; set; }
    }
    public class LFSeparationReason
    {
        public int UserId { get; set; }
        public string SeparationReason { get; set; }
        public string SeparationTypeName { get; set; }
        public int SeparationTypeId { get; set; }
        public int SeparationReasonId { get; set; }
        public int ActionId { get; set; }
        public bool IsDoNotUse { get; set; }
        public int Start { get; set; }
        public int NoofData { get; set; }
        public int Success { get; set; }
        public int Total { get; set; }
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public string SearchText { get; set; }
    }
    public class LFSeparationTask
    {
        public int UserId { get; set; }
        public int SeparationTypeId { get; set; }
        public int TaskOwnerId { get; set; }
        public int EscalateLevelId { get; set; }
        public int InitiateTaskDays { get; set; }
        public int EscalatePostDay { get; set; }
        public int EscalateDayTypeId { get; set; }
        public int EscalateTypeId { get; set; }
        public int InterviewTypeId { get; set; }
        public int DayTypeId { get; set; }
        public int ActionId { get; set; }
        public int SeparationTaskId { get; set; }
        public string SeparationTask { get; set; }
        public string Entity { get; set; }
        public string OrgUnit { get; set; }
        public string Department { get; set; }
        public string CentreType { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string EmployeeType { get; set; }
        public string Position { get; set; }
        public string CostCentre { get; set; }
        public string Grade { get; set; }
        public Boolean IsDoNotUse { get; set; }
        public int Start { get; set; }
        public int NoofData { get; set; }
        public int FieldTypeId { get; set; }
        public int Success { get; set; }
        public int Total { get; set; }

        public string SeparationTypeName { get; set; }
        public string TaskOwnerName { get; set; }
        public string EscalateLevel { get; set; }

        public string DayTypeName { get; set; }
        public string InterviewTypeName { get; set; }
        public string WorkGroup { get; set; }
        public int InitiateTypeId { get; set; }
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public string SearchText { get; set; }

    }
    public class Separationbase
    {
        public int UserId { get; set; }
        public int SeparationDaysUpto { get; set; }
        public int RelievingDateTypeId { get; set; }
        public int ApproveManagerType { get; set; }
        public int AutomaticallyTriggerRetirementWorkflow { get; set; }
        public int RetirementAge { get; set; }
        public int IsDoNotUse { get; set; }
        public int RetirementDays { get; set; }
        public int SeparationId { get; set; }
        public int AutomaticallytriggerAbscondingWorkflow { get; set; }
        public int ContinuousAbsenteeismDay { get; set; }
        public int IncludeRelievingDateType { get; set; }
        public int ActionTypeId { get; set; }
        public string UserDefinedField { get; set; }
    }

    public class Separation : Separationbase
    {
        public string FieldName { get; set; }
        public int FieldTypeId { get; set; }
        public int FieldValueId { get; set; }
        public bool IsMandatory { get; set; }
    }

    public class SeparationData
    {
        public int SeparationDaysUpto { get; set; }
        public bool AllowUpTo { get; set; }
        public int RelievingDateTypeId { get; set; }
        public bool AllowManagerResignOnBehalf { get; set; }
        public bool EditExitDate { get; set; }
        public bool EditRecoveryDates { get; set; }
        public bool RaiseTermination { get; set; }
        public bool AutomaticallyTriggerRetirementWorkflow { get; set; }
        public int RetirementDays { get; set; }
        public int RetirementAge { get; set; }
        public string UserDefinedField { get; set; }
        public List<SeparationUserDefinedData> UserDefinedData { get; set; }
    }

    public class SeparationUserDefinedData
    {
        public string FieldName { get; set; }
        public int? FieldType { get; set; }
        public int? FieldValueId { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
        public string FieldValue { get; set; }
    }

    public class ExitInterview
    {
        public int UserId { get; set; }
        public string Entity { get; set; }
        public string OrgUnit { get; set; }
        public string Department { get; set; }
        public string CentreType { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }
        public string EmployeeType { get; set; }
        public string Position { get; set; }
        public string CostCentre { get; set; }
        public string Grade { get; set; }
        public string EmployeeGroup { get; set; }
        public string Designation { get; set; }
        public Boolean IsDoNotUse { get; set; }
        public int Start { get; set; }
        public int NoofData { get; set; }
        public int FieldTypeId { get; set; }
        public string PolicyName { get; set; }
        public int SeparationTypeId { get; set; }
        public Boolean IsSendMail { get; set; }
        public Boolean IsExitInterviewMandatory { get; set; }
        public int ExitInterviewDay { get; set; }
        public int ExitInterviewDayTypeId { get; set; }
        public int ExitInterviewTypeId { get; set; }
        public int FormId { get; set; }
        public int ActionTypeId { get; set; }
        public int ExitInterviewId { get; set; }
        public string WorkGroup { get; set; }
        public string Form { get; set; }
        public int Success { get; set; }
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public string SearchText { get; set; }

    }
    public class LifeCycleSetting
    {
        public int UserId { get; set; }
        public Boolean InitiatePIP { get; set; }
        public Boolean RecommendSalaryHike { get; set; }
        public Boolean EnablePEP { get; set; }
        public Boolean AutoTriggerPEP { get; set; }
        public int AutoTriggerPEPDay { get; set; }
        public int LifeCycleSettingsId { get; set; }
        public int ActionTypeId { get; set; }

        public int ApprovalPathTypeId { get; set; }
        public int ApprovalReasonsId { get; set; }
        public string ApprovalPathName { get; set; }
        public int NumberofLevels { get; set; }
        public Boolean DoNotUse { get; set; }
        public int LifeCycleWorkFlowId { get; set; }
        public string LifeCycleWorkFlowLevel { get; set; }
        public int Start { get; set; }
        public int NoofData { get; set; }
        public int FieldTypeId { get; set; }
        public int Success { get; set; }
        public int Total { get; set; }

    }

    public class LifeCycleWorkFlow
    {
        public int Total { get; set; }
        public int LifeCycleWorkFlowId { get; set; }
        public int ApprovalPathTypeId { get; set; }
        public string ApprovalReasonsId { get; set; }
        public string ApprovalPathName { get; set; }
        public string Approverto { get; set; }
        public string ApprovalPathType { get; set; }
        public string Escalateto { get; set; }
        public int NumberOfLevels { get; set; }

        public string WorkGrps
        {
            get
            {
                string workGrps = string.Empty;
                if (!string.IsNullOrEmpty(Approverto))
                    workGrps = Approverto;
                if (!string.IsNullOrEmpty(Escalateto))
                    workGrps += "," + Escalateto;
                return workGrps;
            }
        }
    }

    public class LifeCycleWorkFlowLevel
    {
        public string ApproverId { get; set; }
        public int Followup { get; set; }
        public int Escalateto { get; set; }
        public int EscalateAfter { get; set; }
    }

    public class WorkFlowLevel
    {
        public int ApproverId { get; set; }
        public int Followup { get; set; }
        public int Escalateto { get; set; }
        public int EscalateAfter { get; set; }
        public int LifeCycleWorkFlowId { get; set; }
    }

    public class WorkFlowLevelDetails
    {
        public string ApprovalPathName { get; set; }
        public string ApprovalPathTypeId { get; set; }
        public bool? IsDoNotUse { get; set; }
        public int LifeCycleWorkFlowId { get; set; }
        public int NumberOfLevels { get; set; }
        public string ApproverList { get; set; }
        public string ApprovalReasonsId { get; set; }
        public List<WorkFlowLevel> WorkFlowLevelData { get; set; }
    }
}
