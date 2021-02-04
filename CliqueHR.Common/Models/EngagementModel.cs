using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    class EngagementModel
    {
    }
    public class EngagementGroups
    {
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public int UserId { get; set; }
        public int Start { get; set; }
        public int NoofData { get; set; }
        public SortType Sort { get; set; }
        public string SearchText { get; set; }
        public int Action { get; set; }
        public int ProbationDetailId { get; set; }
        public string GroupName { get; set; }
        public string GroupModerators { get; set; }
        public string EmployeeGroup { get; set; }
        public string Entity { get; set; }
        public string OrgUnit { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string EmployeeSearch { get; set; }
        public string Icon { get; set; }
        public string WorkGroup { get; set; }
        public bool PostStatus { get; set; }
        public bool PostPoll { get; set; }
        public bool PostEvent { get; set; }
        public bool IsApprovalStatus { get; set; }
        public bool IsApprovalPostPoll { get; set; }
        public bool IsApprovalPostEvent { get; set; }
        public bool IsDoNotUse { get; set; }
        public int GroupId { get; set; }
        public int ActionId { get; set; }
        public int Success { get; set; }
    }
    public class EngagementValidation : AbstractValidator<Probation>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";
        public EngagementValidation()
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
    public class AddUpdateMarketPlace
    {
        public int UserId { get; set; }
        public bool IsPostSaleVehicle { get; set; }
        public bool IsPostSaleProperty { get; set; }
        public bool IsPostSaleHouseholdGood { get; set; }
        public bool IsPostRentalProperty { get; set; }
        public bool IsDoNotUse { get; set; }
        public int MarketPlaceId { get; set; }
        public bool IsCreateSurvey { get; set; }
        public bool IsPostSurvey { get; set; }
        public int Success { get; set; }
        public int ActionId { get; set; }

    }
    public class DailyContent
    {
        public int UserId { get; set; }
        public int DailyContentId { get; set; }
        public int ActionId { get; set; }
        public int Start { get; set; }
        public int NoofData { get; set; }
        public bool IsShuffle { get; set; }
        public bool IsDate { get; set; }
        public bool Recurring { get; set; }
        public string Entity { get; set; }
        public string OrgUnit { get; set; }
        public string WorkGroup { get; set; }
        public string Content { get; set; }
        public string Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Success { get; set; }
        public int Total { get; set; }


    }
}
