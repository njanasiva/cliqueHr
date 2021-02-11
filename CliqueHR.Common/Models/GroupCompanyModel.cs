﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CliqueHR.Helpers.Validation;

namespace CliqueHR.Common.Models
{
    public class GroupCompanyModel
    {
    }

    public class GroupCompany
    {
        public int Id { get; set; }
        public string TransType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }
        public DateTime IncorporationDate { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int PinCode { get; set; }
        public string ContcatNo { get; set; }
        public string WebSite { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string GSTIN { get; set; }
        public string PF { get; set; }
        public string ESIC { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

    }

    public class GroupCompanyValidation : AbstractValidator<GroupCompany>
    {




        public static readonly string ValidateAll_key = "ValidateAll_key";
        public GroupCompanyValidation()
        {
            this[ValidateAll_key] = ValidateAll;
        }


        private List<ValidationMessage> ValidateAll(GroupCompany model)
        {
            var message = new List<ValidationMessage>();
            string contactPattern = @"^[0-9]{10}$";
            bool isWebSite = false, isContactValid=false;



            if (string.IsNullOrEmpty(model.Name))
            {
                message.Add(new ValidationMessage
                {
                    Property = "GroupCompanyName",
                    Message = "Group Company Name can not be blank."
                });
            }

            if (string.IsNullOrEmpty(model.Code))
            {
                message.Add(new ValidationMessage
                {
                    Property = "GroupCompanyCode",
                    Message = "Group Company Code can not be blank."
                });
            }

            if (string.IsNullOrEmpty(Convert.ToString(model.IncorporationDate)))
            {
                message.Add(new ValidationMessage
                {
                    Property = "IncorporationDate",
                    Message = "Incorporation Date can not be blank."
                });
            }

            if (string.IsNullOrEmpty(model.Address))
            {
                message.Add(new ValidationMessage
                {
                    Property = "Address",
                    Message = "Address can not be blank."
                });
            }

            if (model.TypeId == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "CompanyType",
                    Message = "Company Type can not be blank."
                });
            }

            if (model.CountryId == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Country",
                    Message = "Country can not be blank."
                });
            }

            if (model.StateId == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "State",
                    Message = "State can not be blank."
                });
            }
            if (model.CityId == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "City",
                    Message = "City can not be blank."
                });
            }


            if (model.PinCode == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "PinCode",
                    Message = "PinCode can not be blank Or Enter valid pin code"
                });
            }

            if (string.IsNullOrEmpty(model.ContcatNo))
            {
                message.Add(new ValidationMessage
                {
                    Property = "ContcatNo",
                    Message = "Contcat Number can not be blank."
                });
            }
            else
            {
                isContactValid = Regex.IsMatch(Convert.ToString(model.ContcatNo), contactPattern);
                if (!isContactValid)
                {
                    message.Add(new ValidationMessage
                    {
                        Property = "ContcatNo",
                        Message = "Please enter proper contact number."
                    });
                }
            }
            if (!(string.IsNullOrEmpty(model.WebSite)))
            {
                isWebSite = Uri.IsWellFormedUriString(model.WebSite, UriKind.RelativeOrAbsolute);
                if (!isWebSite)
                {
                    message.Add(new ValidationMessage
                    {
                        Property = "WebSite",
                        Message = "Please enter proper web site url."
                    });
                }
            }


           return message;
        }
    }
}
