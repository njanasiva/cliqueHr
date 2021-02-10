using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Models
{
    public class LoginInfo
    {
        public string CompanyCode { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string RoleParam { get; set; }
        public int EntityId { get; set; }
        public string CachingConfig { get; set; }
        public int SystemTimeOut { get; set; }
    }
}
