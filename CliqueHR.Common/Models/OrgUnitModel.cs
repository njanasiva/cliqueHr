using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Helpers.Validation;

namespace CliqueHR.Common.Models
{
    public class OrgUnitModel
    {

    }

    public class OrgUnits
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int UnitHeadId { get; set; }
        public string UnitHead { get; set; }
        public int ParentEntityId { get; set; }
        public int ParentOrgUnitId { get; set; }
        public string ParentUnit { get; set; }
        public bool IsDoNotUse { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class OrgUnitsValidation : AbstractValidator<OrgUnits>
    {
        public static readonly string ValidateAll_key = "ValidateAll_key";
        public OrgUnitsValidation()
        {
            this[ValidateAll_key] = ValidateAll;
        }

        private List<ValidationMessage> ValidateAll(OrgUnits model)
        {
            var message = new List<ValidationMessage>();
            if (model.Id == 0)
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    message.Add(new ValidationMessage
                    {
                        Property = "OrgUnitName",
                        Message = "Orgunits name can not be blank."
                    });
                }

                if (string.IsNullOrEmpty(model.Code))
                {
                    message.Add(new ValidationMessage
                    {
                        Property = "OrgUnitCode",
                        Message = "Orgunits code can not be blank."
                    });
                }
            }

            if (model.ParentEntityId == 0)
            {
                message.Add(new ValidationMessage
                {
                    Property = "Parent Unit",
                    Message = "Parent Unit Entity should not be blank."
                });
            }

            return message;
        }
    }

    public class ParentEntity
    {
        public int ParentEntityId { get; set; }
        public string EntityName { get; set; }
    }

    public class EntityOrgTreeDataModel
    {
        public int ParentEntityId { get; set; }
        public int OrgUnitId { get; set; }
        public int ParentOrgUnitId { get; set; }
        public int DepartmentId { get; set; }
        public int ParentDepartmentId { get; set; }
        public string Name { get; set; }
        public string EntityName { get; set; }
    }

    public class EntityOrgunitDepartmentModel
    {
        public int EntityId { get; set; }
        public int OrgUnitId { get; set; }
        public int DepartmentId { get; set; }
    }

    public class EntityOrgunitTreeVM: EntityOrgunitDepartmentModel
    {
        public string Name { get; set; }
        public List<EntityOrgunitTreeVM> Childs { get; set; }
    }
}
