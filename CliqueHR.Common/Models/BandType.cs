﻿using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class BandType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
<<<<<<< HEAD
        public string GradeMapping { get; set; }
        public string GradeMappingText { get; set; }
=======
        public int GradeId { get; set; }
        public string GradeName { get; set; }
>>>>>>> change
        public int CreatedBy { get; set; }
        public Boolean IsDoNotUse { get; set; }
        public string CreatedDate { get; set; }
        
    }
    public class BandTypeModelValidation : AbstractValidator<BandType>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
<<<<<<< HEAD
        public static readonly string ValidateEditFields_key = "ValidateEditFields_key";
        public BandTypeModelValidation()
        {
            this[ValidateAll_key] = ValidateAll;
            this[ValidateEditFields_key] = Validate_EditFields;
=======
        public BandTypeModelValidation()
        {
            this[ValidateAll_key] = ValidateAll;
>>>>>>> change
        }

        private List<ValidationMessage> ValidateAll(BandType model)
        {
            var message = new List<ValidationMessage>();
            if (model.TypeName == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "TypeName",
                    Message = "TypeName can not be blank."
                });
            }
<<<<<<< HEAD
            return message;
        }
        private List<ValidationMessage> Validate_EditFields(BandType model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "BandType",
                    Message = "Id can not be Zero."
                });
                if (model.TypeName == "")
                {
                    message.Add(new ValidationMessage
                    {
                        Property = "TypeName",
                        Message = "TypeName can not be blank."
                    });
                }

            }

=======

            if (model.TypeName == "")
            {
                message.Add(new ValidationMessage
                {
                    Property = "GradesMapping",
                    Message = "GradesMapping can not be blank."
                });
            }


>>>>>>> change
            return message;
        }
    }


}
