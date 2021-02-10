using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class RegionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }
        public Int64 RegionHead { get; set; }
        public string RegionHeadName { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDoNotUse { get; set; }
        public string CreatedDate { get; set; }
    }
    public class RegionModelValidator : AbstractValidator<RegionModel>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateId_key = "ValidateId_key";
        public RegionModelValidator()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateId_key] = ValidateId;

        }
        private List<ValidationMessage> ValidateAll(RegionModel model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.Name))
            {
                message.Add(new ValidationMessage
                {
                    Property = "Region Name",
                    Message = "Region name can not null or empty"
                });
            }
            if (model.StateId==0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "StateId",
                    Message = "Please select state name"
                });
            }
            if(model.CityId==0)
            {
                message.Add(new ValidationMessage {
                    Property="CityId",
                    Message="Please select city name"
                });
            }
            if(model.RegionHead==0)
            {
                message.Add(new ValidationMessage
                {
                    Message = "RegionHead",
                    Property = "Region head can not null or empty"
                });
            }
            return message;
        }

        private List<ValidationMessage> ValidateId(RegionModel model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id==0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Id",
                    Message = "Id can not be empty"
                });
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                message.Add(new ValidationMessage
                {
                    Property = "Region Name",
                    Message = "Region name can not null or empty"
                });
            }
            if (model.StateId == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "StateId",
                    Message = "Please select state name"
                });
            }
            if (model.CityId == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CityId",
                    Message = "Please select city name"
                });
            }
            if (model.RegionHead == 0)
            {
                message.Add(new ValidationMessage
                {
                    Message = "RegionHead",
                    Property = "Region head can not null or empty"
                });
            }
            return message;
        }
    }
}
