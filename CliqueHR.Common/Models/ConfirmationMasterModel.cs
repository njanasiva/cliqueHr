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
    }
}
