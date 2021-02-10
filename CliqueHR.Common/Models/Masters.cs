using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class Masters
    {

    }
    public class Currancy
    {
       public int Id { get; set; }
       public string CurrencyCode { get; set; }
       public string CurrencyDesc { get; set; }
    }
    public class CurrancyMapping
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyDesc { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDoNotUse { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CenterType { get; set; }
        public string CenterTypeName { get; set; }
        public bool IsRegisteredOffice { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string PinCode { get; set; }
        public string Phone { get; set; }
        public Int64 LocHeadEmpId { get; set; }
        public string LocHeadName { get; set; }
        public bool IsDoNotUse { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
    public class CurrencyMappingValidation : AbstractValidator<CurrancyMapping>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";
        public CurrencyMappingValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateEditFields_key] = Validate_EditFields;
        }
        private List<ValidationMessage> ValidateAll(CurrancyMapping model)
        {
            var message = new List<ValidationMessage>();
            if (model.CurrencyId<=0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CurrencyId",
                    Message = "Currency should be selected."
                });
            }
            return message;
        }
        private List<ValidationMessage> Validate_EditFields(CurrancyMapping model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CurrencyId",
                    Message = "Id can not be Zero."
                });
            }
            if (model.CurrencyId <= 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CurrencyId",
                    Message = "Currency should be selected."
                });
            }
            return message;
        }
    }

    public class LocationValidation :AbstractValidator<Location>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";

        public LocationValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateEditFields_key] = Validate_EditFields;
        }
        private List<ValidationMessage> ValidateAll(Location model)
        {
            var message = new List<ValidationMessage>();
            if (string.IsNullOrEmpty(model.Name))
            {
                message.Add(new ValidationMessage
                {
                    Property = "LocationName",
                    Message = "Location Name can not be blank."
                });
            }
            if (string.IsNullOrEmpty(model.Code))
            {
                message.Add(new ValidationMessage
                {
                    Property = "LocationCode",
                    Message = "Location Code can not be blank."
                });
            }
            if (model.CenterType <= 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CenterType",
                    Message = "CenterType should be selected."
                });
            }
            return message;
        }

        private List<ValidationMessage> Validate_EditFields(Location model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CurrencyId",
                    Message = "Id can not be Zero."
                });
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                message.Add(new ValidationMessage
                {
                    Property = "LocationName",
                    Message = "Location Name can not be blank."
                });
            }
            if (string.IsNullOrEmpty(model.Code))
            {
                message.Add(new ValidationMessage
                {
                    Property = "LocationCode",
                    Message = "Location Code can not be blank."
                });
            }
            if (model.CenterType <= 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CenterType",
                    Message = "CenterType should be selected."
                });
            }
            return message;
        }
    }

}
