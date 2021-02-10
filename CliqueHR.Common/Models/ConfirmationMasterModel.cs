using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class ConfirmationMasterModel
    {
        public List<DropdownModel> Entity { get; set; }
        public List<DropdownModel> OrgUnit { get; set; }
        public List<DropdownModel> Department { get; set; }
        public List<DropdownModel> CentreType { get; set; }
        public List<DropdownModel> Region { get; set; }
        public List<DropdownModel> Location { get; set; }
        public List<DropdownModel> EmployeeType { get; set; }
        public List<DropdownModel> Position { get; set; }
        public List<DropdownModel> CostCentre { get; set; }
        public List<DropdownModel> Grade { get; set; }
        public List<DropdownModel> AssessmentForm { get; set; }
        public List<DropdownModel> Designation { get; set; }
        public List<DropdownModel> EmployeeGroup { get; set; }
        public List<DropdownModel> SeparationType { get; set; }
        public List<DropdownModel> DayType { get; set; }
        public List<DropdownModel> Users { get; set; }
        public List<DropdownModel> Moderators { get; set; }

        public List<EntityOrgDepTree> EntityOrgDepTreeData { get; set; }
    }

    public class BaseMembers
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EntityOrgDepTree : BaseMembers
    {
        public List<OrgTree> Orgs { get; set; }
    }

    public class OrgTree : BaseMembers
    {
        public List<BaseMembers> Departments { get; set; }
    }

    public class EntityOrgDep
    {
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public int? OrgUnitId { get; set; }
        public string OrgUnit { get; set; }
        public int? DepartmentId { get; set; }
        public string Department { get; set; }
    }
}
