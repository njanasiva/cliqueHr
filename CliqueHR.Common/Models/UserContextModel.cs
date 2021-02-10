using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class UserContextModel
    {
        public int EmployeeId { get; set; }
        public int EntityId { get; set; }
        public string CompanyCode { get; set; }
        public string EmployeeCode { get; set; }
        public long EmployeeGeneralInfoTimeStamp { get; set; }
        public int EntityTimeStamp { get; set; }
        public string AccessParameters { get; set; }
    }
}
