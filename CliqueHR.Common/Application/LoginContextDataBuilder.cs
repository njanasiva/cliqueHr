using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.Common.Application
{
    public class LoginContextDataBuilder
    {
        public int EmployeeId { get; set; }
        public int EntityId { get; set; }
        public string CompanyCode { get; set; }
        public string EmployeeCode { get; set; }
        public string CachingConfig { get; set; }
        public string AccessParameters { get; set; }
        public UserContextModel Build()
        {
            UserContextModel loginContextData = new UserContextModel();
            loginContextData.EmployeeId = this.EmployeeId;
            loginContextData.EntityId = this.EntityId;
            loginContextData.CompanyCode = this.CompanyCode;
            loginContextData.EmployeeCode = this.EmployeeCode;
            loginContextData.AccessParameters = this.AccessParameters;

            // TimeStamp Values
            string[] timeStamps = this.CachingConfig.Split(',');
            loginContextData.EmployeeGeneralInfoTimeStamp = Convert.ToInt64(timeStamps[0]);
            loginContextData.EntityTimeStamp = Convert.ToInt32(timeStamps[1]);
            return loginContextData;
        }
    }
}
